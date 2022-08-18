using FluentValidation;
using WheelsAndGoods.Application.Models.Orders;

namespace WheelsAndGoods.Api.Validation
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderValidator()
        {
            RuleFor(model => model.Title)
                .NotEmpty()
                .MaximumLength(64);

            RuleFor(model => model.Cargo)
                .NotEmpty()
                .MaximumLength(64);

            RuleFor(model => model.Description)
                .MaximumLength(5124);

            RuleFor(model => model.From)
                .NotEmpty()
                .MaximumLength(128);

            RuleFor(model => model.To)
                .NotEmpty()
                .MaximumLength(128);

            RuleFor(model => model.DeliveryDeadlinAtUtc)
                .NotEmpty()
                .GreaterThan(model => DateTime.UtcNow)
                .WithMessage("Delivery deadline must be greater than now")
                .Must(model => model.Kind == DateTimeKind.Utc);

            RuleFor(model => model.Price)
                .NotEmpty()
                .GreaterThan(1)
                .WithMessage("Price must be greater than 1")
                .LessThan(decimal.MaxValue)
                .WithMessage("Price is to big");
        }
    }
}
