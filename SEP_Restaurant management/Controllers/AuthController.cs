using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SEP_Restaurant_management.DTO;
using SEP_Restaurant_management.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SEP_Restaurant_management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly SignInManager<UserIdentity> _signInManager;
        private readonly IConfiguration _config;

        public AuthController(
            UserManager<UserIdentity> userManager,
            SignInManager<UserIdentity> signInManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.UserName) ||
                string.IsNullOrWhiteSpace(req.Email) ||
                string.IsNullOrWhiteSpace(req.Password))
            {
                return BadRequest(new { message = "UserName, Email, Password là bắt buộc." });
            }

            var existingByEmail = await _userManager.FindByEmailAsync(req.Email);
            if (existingByEmail != null)
                return Conflict(new { message = "Email đã tồn tại." });

            var existingByUserName = await _userManager.FindByNameAsync(req.UserName);
            if (existingByUserName != null)
                return Conflict(new { message = "UserName đã tồn tại." });

            var user = new UserIdentity
            {
                UserName = req.UserName,
                Email = req.Email
            };

            var result = await _userManager.CreateAsync(user, req.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    message = "Đăng ký thất bại.",
                    errors = result.Errors.Select(e => e.Description)
                });
            }

            // (Tuỳ chọn) Tự login luôn và trả token
            var token = await CreateJwtTokenAsync(user);

            return Ok(token);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.UserNameOrEmail) ||
                string.IsNullOrWhiteSpace(req.Password))
            {
                return BadRequest(new { message = "UserNameOrEmail và Password là bắt buộc." });
            }

            UserIdentity? user =
                req.UserNameOrEmail.Contains("@")
                    ? await _userManager.FindByEmailAsync(req.UserNameOrEmail)
                    : await _userManager.FindByNameAsync(req.UserNameOrEmail);

            if (user == null)
                return Unauthorized(new { message = "Sai tài khoản hoặc mật khẩu." });

            var check = await _signInManager.CheckPasswordSignInAsync(user, req.Password, lockoutOnFailure: false);
            if (!check.Succeeded)
                return Unauthorized(new { message = "Sai tài khoản hoặc mật khẩu." });

            var token = await CreateJwtTokenAsync(user);
            return Ok(token);
        }

        private async Task<AuthResponse> CreateJwtTokenAsync(UserIdentity user)
        {
            var jwtSection = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]!));

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName ?? ""),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName ?? "")
            };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expireMinutes = int.TryParse(jwtSection["ExpireMinutes"], out var m) ? m : 120;
            var expiresAt = DateTime.UtcNow.AddMinutes(expireMinutes);

            var token = new JwtSecurityToken(
                issuer: jwtSection["Issuer"],
                audience: jwtSection["Audience"],
                claims: claims,
                expires: expiresAt,
                signingCredentials: creds
            );

            return new AuthResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAtUtc = expiresAt,
                UserId = user.Id,
                UserName = user.UserName ?? "",
                Email = user.Email ?? ""
            };
        }
    }
    }
