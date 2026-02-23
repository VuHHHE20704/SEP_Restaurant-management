using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using SEP_Restaurant_management.Models;
using SEP_Restaurant_management.Repositories.Interface;

namespace SEP_Restaurant_management.Services;

public abstract class BaseService<T> where T : class
{
    protected readonly IUnitOfWork _unitOfWork;
    protected ILogger<T> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    protected readonly IMapper _mapper;

    public BaseService(IUnitOfWork unitOfWork, ILogger<T> logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    protected string GetUserIdFromJwt()
    {
        string userId = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return userId;
    }
    
    protected async Task<UserIdentity?> GetCurrentUserAsync()
    {
        var userId = GetUserIdFromJwt();
        if (string.IsNullOrEmpty(userId))
            return null;

        return await _unitOfWork
            .GetRepository<UserIdentity>()
            .SingleOrDefaultAsync( predicate:u => u.Id == userId );
    }

    protected async Task<Customer?> GetCurrentCustomerAsync()
    {
        var user = await GetCurrentUserAsync();
        if (user == null)
            return null;

        return await _unitOfWork.GetRepository<Customer>()
            .SingleOrDefaultAsync(predicate: c => c.UserId == user.Id);
    }

    protected string GetRoleFromJwt()
    {
        string role = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
        return role;
    }
}
