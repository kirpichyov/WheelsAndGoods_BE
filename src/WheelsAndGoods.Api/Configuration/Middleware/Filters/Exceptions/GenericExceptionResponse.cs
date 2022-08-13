using WheelsAndGoods.Core.Exceptions;

namespace WheelsAndGoods.Api.Configuration.Middleware.Filters.Exceptions;

internal class GenericExceptionResponse : ExceptionResponse<string>
{
	public GenericExceptionResponse()
		: base(ExceptionIdentifiers.Generic, "Unexpected error occured")
	{
	}
}
