namespace WheelsAndGoods.Api.Configuration.Middleware.Filters.Exceptions;

internal class ExceptionResponse<TErrorNode>
{
	public ExceptionResponse(string reason, TErrorNode[] errors)
	{
		Reason = reason;
		Errors = errors;
	}

	public ExceptionResponse(string reason, TErrorNode error)
	{
		Reason = reason;
		Errors = new[] {error};
	}

	public string Reason { get; }
	public TErrorNode[] Errors { get; }
}
