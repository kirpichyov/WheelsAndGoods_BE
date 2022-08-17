using System.Net.Mail;
using System.Text.RegularExpressions;
using FluentValidation;
using WheelsAndGoods.Api.Validation.Internal;
using WheelsAndGoods.Application.Models.User;

namespace WheelsAndGoods.Api.Validation;

public class RegisterRequestValidator : PasswordRequestValidator<RegisterRequest>
{
	public RegisterRequestValidator()
	{
		RuleFor(model => model.Email)
			.NotEmpty()
			.Must(email => MailAddress.TryCreate(email, out _))
			.WithMessage("{PropertyName} has invalid format");

		RuleFor(model => model.Phone)
			.NotEmpty()
			.MinimumLength(4)
			.MaximumLength(16)
			.Must(phoneNumber => Regex.IsMatch(phoneNumber, @"^[\W+][0-9]+$"))
			.WithMessage("{PropertyName} has invalid format");

		RuleFor(model => model.FirstName)
			.MinimumLength(1)
			.MaximumLength(32)
			.Must(firstName => !firstName.Contains(' '))
			.WithMessage("{PropertyName} contain a whitespace");

		RuleFor(model => model.LastName)
			.MinimumLength(1)
			.MaximumLength(32)
			.Must(lastName => !lastName.Contains(' '))
			.WithMessage("{PropertyName} contain a whitespace");
	}
}
