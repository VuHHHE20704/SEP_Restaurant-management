using SEP_Restaurant_management.DTOs.Order.Request;
using SEP_Restaurant_management.DTOs.Order.Response;
using SEP_Restaurant_management.Repositories.Paginate;

namespace SEP_Restaurant_management.Services.Interface;

public interface ICustomerOrderService
{
    Task<OrderDetailDto> CreateOrderAsync(CreateOrderRequestDto request);

    Task<IPaginate<OrderSummaryDto>> GetOrderHistoryAsync(int pageIndex, int pageSize);

    Task<OrderDetailDto?> GetOrderDetailAsync(int orderId);
}
