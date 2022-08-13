namespace WheelsAndGoods.Core.Exceptions;

public class ExternalApiException : ExceptionBase
{
	public ExternalApiException(string message, string apiResponse)
		: base(message, ExceptionIdentifiers.Generic)
	{
		ApiResponse = apiResponse;
	}

	public string ApiResponse { get; }
}
