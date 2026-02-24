using Microsoft.EntityFrameworkCore;
using SEP_Restaurant_management.Models;
using SEP_Restaurant_management.Repositories.Interface;

namespace SEP_Restaurant_management.Repositories.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SepDatabaseContext _db;

        public OrderRepository(SepDatabaseContext db)
        {
            _db = db;
        }

        public async Task<List<Order>> GetActiveOrdersAsync(CancellationToken ct = default)
        {
            const string ACTIVE = "active";

            return await _db.Orders
                .AsNoTracking()
                .Where(o =>
                    (o.OrderStatus != null && o.OrderStatus.ToLower() == ACTIVE) ||
                    (o.Status != null && o.Status.ToLower() == ACTIVE)
                )
                .OrderByDescending(o => o.CreatedAt ?? o.OrderDate)
                .ToListAsync(ct);
        }
    }
}
