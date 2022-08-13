namespace WheelsAndGoods.Api.Configuration.Middleware.Filters.Exceptions;

internal class GenericExceptionDetailedResponse : GenericExceptionResponse
{
	public GenericExceptionDetailedResponse(Exception exception)
	{
		Details = exception;
	}

	public Exception Details { get; }
}
