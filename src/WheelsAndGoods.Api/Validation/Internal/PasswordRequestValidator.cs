using System.Text.RegularExpressions;
using FluentValidation;
using WheelsAndGoods.Application.Models.Internal;

namespace WheelsAndGoods.Api.Validation.Internal;

public class PasswordRequestValidator<T> : AbstractValidator<T>
	where T : class, IContainsPassword
{
	public PasswordRequestValidator()
	{
		RuleFor(model => model.Password)
			.NotEmpty()
			.MinimumLength(8)
			.MaximumLength(32);

		RuleFor(model => model.Password)
			.NotEmpty()
			.Must(password => !password.Contains(' '))
			.WithMessage("{PropertyName} contain a whitespace")
			.Must(password => Regex.IsMatch(password, @"(?=.*[\W_])"))
			.WithMessage("{PropertyName} must have at least 1 special character")
			.Must(password => Regex.IsMatch(password, @"(?=.*\d)"))
			.WithMessage("{PropertyName} must have at least 1 number")
			.Must(password => Regex.IsMatch(password, @"(?=.*[A-Z])"))
			.WithMessage("{PropertyName} must have at least 1 upper case character")
			.When(model => !string.IsNullOrEmpty(model.Password));
	}
}
