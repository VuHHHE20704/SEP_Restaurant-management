using SEP_Restaurant_management.Models;

namespace SEP_Restaurant_management.Repositories.Interface
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetActiveOrdersAsync(CancellationToken ct = default);
    }
}
