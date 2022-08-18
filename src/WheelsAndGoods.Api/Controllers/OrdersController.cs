using Microsoft.AspNetCore.Http;
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

        [HttpPost()]
        [ProducesResponseType(typeof(CreateOrderResponce), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var result = await _ordersService.CreateOrder(request);
            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}
