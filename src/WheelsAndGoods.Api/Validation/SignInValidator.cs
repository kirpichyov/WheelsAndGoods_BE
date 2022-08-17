using System.Net.Mail;
using FluentValidation;
using WheelsAndGoods.Application.Models.Auth;

namespace WheelsAndGoods.Api.Validation
{
	public class SignInValidator : AbstractValidator<SignInRequest>
	{
		public SignInValidator()
		{
			RuleFor(model => model.Email)
			.NotEmpty()
			.Must(email => MailAddress.TryCreate(email, out _))
			.WithMessage("{PropertyName} has invalid format");
		}
	}
}
