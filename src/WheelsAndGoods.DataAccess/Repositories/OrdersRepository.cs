using Microsoft.EntityFrameworkCore;
using WheelsAndGoods.Core.Models.Entities;
using WheelsAndGoods.Core.Models.Filters;
using WheelsAndGoods.Core.Models.Paginations;
using WheelsAndGoods.Core.Models.Paginations.Responses;
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

        public async Task<PaginationOrderResponse<Order>> GetOrders(FilterOrderModel filterOrderModel,
            PaginationOrderModel paginationOrderModel)
        {
            var query = Context.Orders
                .Include(order => order.Customer)
                .Where(order => !order.IsDeleted)
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
            
            int totalCount = await query.CountAsync();

            query = query.OrderByDescending(order => order.CreatedAtUtc)
                .Skip((paginationOrderModel.PageNumber - 1) * paginationOrderModel.PageSize)
                .Take(paginationOrderModel.PageSize)
                .AsNoTracking();

            return new PaginationOrderResponse<Order>()
            {
                TotalPages = (totalCount % paginationOrderModel.PageSize == 0) 
                    ? (totalCount / paginationOrderModel.PageSize) 
                    : (totalCount / paginationOrderModel.PageSize) + 1,
                TotalCount = totalCount,
                Data = await query.ToArrayAsync()
            };
        }

        public async Task<Order?> GetById(Guid orderId, bool useTracking)
        {
            return await Context.Orders
                .Include(order => order.Customer)
                .WithTracking(useTracking)
                .Where(order => !order.IsDeleted)
                .FirstOrDefaultAsync(order => order.Id == orderId);
        }
    }
}
