using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SEP_Restaurant_management.DTOs.Cart.Request;
using SEP_Restaurant_management.DTOs.Cart.Response;
using SEP_Restaurant_management.Models;
using SEP_Restaurant_management.Repositories.Interface;
using SEP_Restaurant_management.Services.Interface;

namespace SEP_Restaurant_management.Services.Implementation;

public class CustomerCartService : BaseService<CustomerCartService>, ICustomerCartService
{
    public CustomerCartService(IUnitOfWork unitOfWork, ILogger<CustomerCartService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<CartPreviewResponseDto> PreviewCartAsync(CartPreviewRequestDto request)
    {
        var response = new CartPreviewResponseDto
        {
            CreatedAt = DateTime.UtcNow,
            Items = new List<CartItemDto>()
        };

        if (request.Items == null || !request.Items.Any())
            return response;

        var dishSizeIds = request.Items.Select(i => i.DishSizeId).Distinct().ToList();
        var dishSizeRepo = _unitOfWork.GetRepository<DishSize>();

        var dishSizes = await dishSizeRepo.GetListAsync(
            predicate: ds => dishSizeIds.Contains(ds.DishSizeId),
            include: q => q.Include(ds => ds.Dish).Include(ds => ds.Price)
        );

        foreach (var item in request.Items)
        {
            var dishSize = dishSizes.FirstOrDefault(ds => ds.DishSizeId == item.DishSizeId);
            if (dishSize == null)
                throw new ArgumentException($"Invalid DishSizeId: {item.DishSizeId}");

            var cartItem = _mapper.Map<CartItemDto>(dishSize);
            cartItem.Quantity = item.Quantity;
            cartItem.Note = item.Note;
            cartItem.LineTotal = cartItem.UnitPrice * item.Quantity;

            response.Items.Add(cartItem);
        }

        response.TotalPrice = response.Items.Sum(i => i.LineTotal);
        return response;
    }
}
