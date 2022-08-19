using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelsAndGoods.Application.Models.Orders;

namespace WheelsAndGoods.Application.Contracts.Services
{
    public interface IOrdersService
    {
        Task<CreateOrderResponse> CreateOrder(CreateOrderRequest createOrderRequest);
    }
}
