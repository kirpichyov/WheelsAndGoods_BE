using WheelsAndGoods.Core.Models.Entities;
using WheelsAndGoods.Core.Models.Filters;
using WheelsAndGoods.Core.Models.Paginations;
using WheelsAndGoods.Core.Models.Paginations.Responses;

namespace WheelsAndGoods.DataAccess.Contracts
{
    public interface IOrdersRepository : IRepositoryBase<Order>
    {
        Task<PaginationOrderResponse<Order>> GetOrders(FilterOrderModel filterOrderModel,
            PaginationOrderModel paginationOrderModel);
        Task<Order?> GetById(Guid orderId, bool useTracking);
    }
}
