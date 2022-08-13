namespace WheelsAndGoods.Core.Exceptions;

public class ExceptionBase : Exception
{
	public ExceptionBase(string message, string identifier) : base(message)
	{
		Identifier = identifier;
	}

	public string Identifier { get; }
}
