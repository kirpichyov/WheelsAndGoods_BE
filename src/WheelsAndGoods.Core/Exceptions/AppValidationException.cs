namespace WheelsAndGoods.Core.Exceptions;

public class AppValidationException : ExceptionBase
{
	public AppValidationException(string message)
		: base(message, ExceptionIdentifiers.AppValidation)
	{
	}
}
