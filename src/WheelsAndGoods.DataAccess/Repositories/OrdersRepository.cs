using Microsoft.EntityFrameworkCore;
using WheelsAndGoods.Core.Models.Entities;
using WheelsAndGoods.Core.Models.Filter;
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

        public async Task<IReadOnlyCollection<Order>> GetOrders(FilterOrderModel filterOrderModel)
        {
            var query = Context.Orders
                .Include(order => order.Customer)
                .AsQueryable();

            if (filterOrderModel.From != null)
            {
                query = query.Where(order => EF.Functions.ILike(
                    order.From, $"%{filterOrderModel.From}%"));
            }

            if (filterOrderModel.Title != null)
            {
                query = query.Where(order => EF.Functions.ILike(
                    order.Title, $"%{filterOrderModel.Title}%"));
            }

            if (filterOrderModel.To != null)
            {
                query = query.Where(order => EF.Functions.ILike(
                    order.To, $"%{filterOrderModel.To}%"));
            }

            if (filterOrderModel.CustomerFullName != null)
            {
                query = query.Where(order => EF.Functions.ILike(
                    $"{order.Customer.Firstname} {order.Customer.Lastname}",
                    $"%{filterOrderModel.CustomerFullName}%"));
            }
            
            if (filterOrderModel.Price != null)
            {
                query = query.Where(order => order.Price >= filterOrderModel.Price);
            }

            return await query.OrderByDescending(order => order.CreatedAtUtc)
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
