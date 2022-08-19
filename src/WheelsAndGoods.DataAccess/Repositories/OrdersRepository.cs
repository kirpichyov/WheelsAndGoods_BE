using Microsoft.EntityFrameworkCore;
using WheelsAndGoods.Core.Models.Entities;
using WheelsAndGoods.DataAccess.Connection;
using WheelsAndGoods.DataAccess.Contracts;
using WheelsAndGoods.DataAccess.Extensions;

namespace WheelsAndGoods.DataAccess.Repositories
{
    public class OrdersRepository : RepositoryBase<Order>, IOrdersRepository
    {
        public OrdersRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public async Task<IReadOnlyCollection<Order>> GetOrders()
        {
            return await Context.Orders
                .Include(order => order.Customer)
                .OrderByDescending(order => order.CreatedAtUtc)
                .AsNoTracking()
                .ToArrayAsync();
        }
        public async Task<Order?> GetById(Guid orderId, bool useTracking)
        {
            return await Context.Orders
                .Include(order => order.Customer)
                .WithTracking(useTracking)
                .FirstOrDefaultAsync(order => order.Id == orderId);
        }
    }
}
