using Microsoft.AspNetCore.Mvc;
using WheelsAndGoods.Api.Configuration.Swagger.Models;
using WheelsAndGoods.Application.Contracts.Services;
using WheelsAndGoods.Application.Models.Orders;

namespace WheelsAndGoods.Api.Controllers
{
    public class OrdersController : ApiControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var result = await _ordersService.CreateOrder(request);
            return StatusCode(StatusCodes.Status201Created, result);
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrders()
        {
            var result = await _ordersService.GetOrders();
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOrder([FromRoute] Guid id, [FromBody] UpdateOrderRequest request)
        {
            var result = await _ordersService.UpdateOrder(request, id);
            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}
