using FluentValidation;
using WheelsAndGoods.Application.Models.Orders;

namespace WheelsAndGoods.Api.Validation
{
    public class TakeOrderRequestValidator : AbstractValidator<TakeOrderRequest>
    {
        public TakeOrderRequestValidator()
        {
            RuleFor(model => model.Comment).
                MaximumLength(5120);
        }
    }
}
