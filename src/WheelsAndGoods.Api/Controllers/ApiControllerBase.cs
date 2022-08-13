using Microsoft.AspNetCore.Mvc;
using WheelsAndGoods.Api.Configuration.Swagger.Models;

namespace WheelsAndGoods.Api.Controllers;

[ApiController]
[Route("{controller}")]
[ProducesResponseType(typeof(EmptyResponseModel), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(EmptyResponseModel), StatusCodes.Status403Forbidden)]
// [Authorize]
public class ApiControllerBase : ControllerBase
{
}
