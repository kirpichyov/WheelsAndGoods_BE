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
			.NotEmpty();

			RuleFor(model => model.Password)
				.NotEmpty();
		}
	}
}
