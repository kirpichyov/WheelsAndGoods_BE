using Microsoft.AspNetCore.Mvc;
using WheelsAndGoods.Api.Configuration.Swagger.Models;
using WheelsAndGoods.Application.Contracts.Services;
using WheelsAndGoods.Application.Models.Filtering;
using WheelsAndGoods.Application.Models.Orders;
using WheelsAndGoods.Application.Models.Paginations;

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
        public async Task<IActionResult> GetOrders([FromQuery] FilterOrderRequest filterOrderRequest,
            [FromQuery] PaginationRequest paginationRequest)
        {
            var result = await _ordersService.GetOrders(filterOrderRequest, paginationRequest);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrderById([FromRoute] Guid id)
        {
            var result = await _ordersService.GetOrderById(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteOrder([FromRoute] Guid id)
        {
            await _ordersService.DeleteOrder(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPost("{id}/request")]
        [ProducesResponseType(typeof(TakeOrderResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> TakeOrder([FromRoute]Guid id, [FromBody] TakeOrderRequest request)
        {
            var result = await _ordersService.CreateTakeOrderRequest(id, request);
            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}
