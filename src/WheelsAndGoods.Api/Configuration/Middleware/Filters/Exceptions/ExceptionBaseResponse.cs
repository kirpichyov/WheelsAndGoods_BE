using WheelsAndGoods.Core.Exceptions;

namespace WheelsAndGoods.Api.Configuration.Middleware.Filters.Exceptions;

internal class ExceptionBaseResponse : ExceptionResponse<string>
{
	public ExceptionBaseResponse(ExceptionBase exception)
		: base(exception.Identifier, exception.Message)
	{
	}
}
