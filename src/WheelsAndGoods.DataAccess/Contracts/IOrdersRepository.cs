using WheelsAndGoods.Core.Models.Entities;

namespace WheelsAndGoods.DataAccess.Contracts
{
    public interface IOrdersRepository : IRepositoryBase<Order>
    {
        Task<IReadOnlyCollection<Order>> GetOrders();
        Task<Order?> GetById(Guid orderId, bool useTracking);
    }
}
