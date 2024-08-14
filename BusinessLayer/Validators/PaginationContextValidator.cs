using BusinessLayer.RequestContext.Journal;
using FluentValidation;

namespace BusinessLayer.Validators;

public class PaginationContextValidator : AbstractValidator<Pagination>
{
    public PaginationContextValidator()
    {
        RuleFor(x => x.skip)
            .NotNull()
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.take)
           .NotNull()
           .GreaterThan(0);

    }
}
