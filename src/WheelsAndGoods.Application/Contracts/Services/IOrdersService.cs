using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelsAndGoods.Application.Models.Filtering;
using WheelsAndGoods.Application.Models.Orders;

namespace WheelsAndGoods.Application.Contracts.Services
{
    public interface IOrdersService
    {
        Task<OrderResponse> CreateOrder(CreateOrderRequest createOrderRequest);
        Task<IReadOnlyCollection<OrderResponse>> GetOrders(FilterOrderRequest filterOrderRequest);
        Task<OrderResponse> UpdateOrder(UpdateOrderRequest updateOrderRequest, Guid orderId);
    }
}
