using WheelsAndGoods.Core.Exceptions;

namespace WheelsAndGoods.Api.Configuration.Middleware.Filters.Exceptions;

internal class ExceptionBaseDetailedResponse : ExceptionBaseResponse
{
	public ExceptionBaseDetailedResponse(ExceptionBase exception)
		: base(exception)
	{
		Details = exception;
	}

	public Exception Details { get; }
}
