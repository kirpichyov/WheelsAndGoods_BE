using FluentValidation;
using WheelsAndGoods.Application.Models.Paginations;

namespace WheelsAndGoods.Api.Validation;

public class PaginationOrderRequestValidator : AbstractValidator<PaginationRequest>
{
    public PaginationOrderRequestValidator()
    {
        RuleFor(request => request.PageNumber)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than '1'");
        
        RuleFor(request => request.PageSize)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than '1'");
    }
}
