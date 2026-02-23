using Microsoft.AspNetCore.Mvc;
using SEP_Restaurant_management.Services.Interface;
using SEP_Restaurant_management.DTOs.Cart.Request;
using SEP_Restaurant_management.DTOs.Cart.Response;

namespace SEP_Restaurant_management.Controllers.Customer
{
    [ApiController]
    [Route("api/v1/customers/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICustomerCartService _customerCartService;

        public CartController(ICustomerCartService customerCartService)
        {
            _customerCartService = customerCartService;
        }

        [HttpPost("preview")]
        public async Task<ActionResult<CartPreviewResponseDto>> PreviewCart([FromBody] CartPreviewRequestDto request)
        {
            try
            {
                var response = await _customerCartService.PreviewCartAsync(request);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
