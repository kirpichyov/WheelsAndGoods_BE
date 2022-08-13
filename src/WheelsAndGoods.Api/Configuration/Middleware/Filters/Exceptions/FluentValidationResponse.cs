using WheelsAndGoods.Core.Exceptions;

namespace WheelsAndGoods.Api.Configuration.Middleware.Filters.Exceptions;

internal class FluentValidationResponse : ExceptionResponse<FluentValidationResponse.ErrorNode>
{
	public FluentValidationResponse(ErrorNode[] errorNodes)
		: base(ExceptionIdentifiers.ModeValidation, errorNodes)
	{
	}

	internal class ErrorNode
	{
		public ErrorNode(string property, string[] messages)
		{
			Property = property;
			Messages = messages;
		}

		public string Property { get; }
		public string[] Messages { get; }
	}
}
