using Microsoft.EntityFrameworkCore;
using WheelsAndGoods.Core.Models.Entities;
using WheelsAndGoods.DataAccess.Connection;
using WheelsAndGoods.DataAccess.Contracts;

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
    }
}
