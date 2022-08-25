using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelsAndGoods.Application.Models.Filtering;
using WheelsAndGoods.Application.Models.Orders;
using WheelsAndGoods.Application.Models.Paginations;
using WheelsAndGoods.Core.Models.Entities;
using WheelsAndGoods.Core.Models.Paginations.Responses;

namespace WheelsAndGoods.Application.Contracts.Services
{
    public interface IOrdersService
    {
        Task<OrderResponse> CreateOrder(CreateOrderRequest createOrderRequest);
        Task<PaginationOrderResponse<Order>> GetOrders(FilterOrderRequest filterOrderRequest,
            PaginationRequest paginationRequest);
        Task<OrderResponse> UpdateOrder(UpdateOrderRequest updateOrderRequest, Guid orderId);
        Task<OrderResponse> GetOrderById(Guid orderId);
        Task DeleteOrder(Guid orderId);
        Task<TakeOrderResponse> CreateTakeOrderRequest(Guid orderId, TakeOrderRequest request);
    }
}
