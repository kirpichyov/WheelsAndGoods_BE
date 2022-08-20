using WheelsAndGoods.Core.Models.Entities;
using WheelsAndGoods.Core.Models.Filter;

namespace WheelsAndGoods.DataAccess.Contracts
{
    public interface IOrdersRepository : IRepositoryBase<Order>
    {
        Task<IReadOnlyCollection<Order>> GetOrders(FilterOrderModel filterOrderModel);
        Task<Order?> GetById(Guid orderId, bool useTracking);
    }
}
