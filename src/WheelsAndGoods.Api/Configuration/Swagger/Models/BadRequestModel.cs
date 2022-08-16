using WheelsAndGoods.Api.Configuration.Middleware.Filters.Exceptions;

namespace WheelsAndGoods.Api.Configuration.Swagger.Models;

internal class BadRequestModel : FluentValidationResponse
{
	public BadRequestModel() 
		: base(Array.Empty<ErrorNode>())
	{
	}
}
