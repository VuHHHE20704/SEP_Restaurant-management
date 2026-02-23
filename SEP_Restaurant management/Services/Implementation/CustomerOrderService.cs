using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SEP_Restaurant_management.DTOs.Order.Request;
using SEP_Restaurant_management.DTOs.Order.Response;
using SEP_Restaurant_management.Models;
using SEP_Restaurant_management.Repositories.Interface;
using SEP_Restaurant_management.Repositories.Paginate;
using SEP_Restaurant_management.Services.Interface;


namespace SEP_Restaurant_management.Services.Implementation;

public class CustomerOrderService : BaseService<CustomerOrderService>, ICustomerOrderService
{
    public CustomerOrderService(IUnitOfWork unitOfWork, ILogger<CustomerOrderService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<OrderDetailDto> CreateOrderAsync(CreateOrderRequestDto request)
    {
        var customer = await GetCurrentCustomerAsync();

        var dishSizeIds = request.Items.Select(i => i.DishSizeId).Distinct().ToList();
        var dishSizeRepo = _unitOfWork.GetRepository<DishSize>();
        var dishSizes = await dishSizeRepo.GetListAsync(
            predicate: ds => dishSizeIds.Contains(ds.DishSizeId),
            include: q => q.Include(ds => ds.Dish).Include(ds => ds.Price)
        );

        var order = new Order
        {
            CustomerId = customer.CustomerId,
            OrderDate = DateTime.UtcNow,
            OrderStatus = "Pending", 
            Status = "Pending",
            Note = request.Note,
            CreatedAt = DateTime.UtcNow,
            OrderDetails = new List<OrderDetail>()
        };

        decimal totalPrice = 0;

        foreach (var item in request.Items)
        {
            var dishSize = dishSizes.FirstOrDefault(ds => ds.DishSizeId == item.DishSizeId);
            if (dishSize == null)
                throw new ArgumentException($"Invalid DishSizeId: {item.DishSizeId}");

            var lineTotal = dishSize.Price.PriceValue * item.Quantity;
            totalPrice += lineTotal;

            order.OrderDetails.Add(new OrderDetail
            {
                DishSizeId = item.DishSizeId,
                Quantity = item.Quantity,
                UnitPrice = dishSize.Price.PriceValue,
                Note = item.Note,
                Status = "Pending"
            });
        }

        order.TotalPrice = totalPrice;
        order.OrderCode = $"ORD-{DateTime.UtcNow.Ticks}"; 

        var orderRepo = _unitOfWork.GetRepository<Models.Order>();
        await orderRepo.InsertAsync(order);
        await _unitOfWork.CommitAsync();

        return await GetOrderDetailAsync(order.OrderId) ?? throw new InvalidOperationException("Failed to retrieve created order");
    }

    public async Task<IPaginate<OrderSummaryDto>> GetOrderHistoryAsync(int pageIndex, int pageSize)
    {
        var customer = await GetCurrentCustomerAsync();
        if (customer == null)
            throw new InvalidOperationException("User or Customer not found");

        var orderRepo = _unitOfWork.GetRepository<Models.Order>();
        
        var pagedOrders = await orderRepo.GetPagingListAsync(
            predicate: o => o.CustomerId == customer.CustomerId,
            orderBy: q => q.OrderByDescending(o => o.CreatedAt),
            page: pageIndex,
            size: pageSize
        );

        return new Paginate<OrderSummaryDto>
        {
            Items = _mapper.Map<IList<OrderSummaryDto>>(pagedOrders.Items),
            Page = pagedOrders.Page,
            Size = pagedOrders.Size,
            Total = pagedOrders.Total,
            TotalPages = pagedOrders.TotalPages
        };
    }

    public async Task<OrderDetailDto?> GetOrderDetailAsync(int orderId)
    {
        var orderRepo = _unitOfWork.GetRepository<Models.Order>();
        var order = await orderRepo.SingleOrDefaultAsync(
            predicate: o => o.OrderId == orderId,
            include: q => q.Include(o => o.OrderDetails)
                           .ThenInclude(od => od.DishSize)
                           .ThenInclude(ds => ds.Dish)
        );

        if (order == null) return null;

        return _mapper.Map<OrderDetailDto>(order);
    }
}
