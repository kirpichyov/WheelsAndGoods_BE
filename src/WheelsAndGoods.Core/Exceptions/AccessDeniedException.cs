namespace WheelsAndGoods.Core.Exceptions;

public class AccessDeniedException : ExceptionBase
{
	public AccessDeniedException(string message = "Access denied")
		: base(message, ExceptionIdentifiers.AppValidation)
	{
	}
}
