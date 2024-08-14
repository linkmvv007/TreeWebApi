using BusinessLayer.Commands;
using FluentValidation;

namespace BusinessLayer.Validators;

public class CreateNodeContextValidator : AbstractValidator<CreateNodeContext>
{

    public CreateNodeContextValidator()
    {
        RuleFor(x => x.parentNodeId)
            .NotEmpty();

        RuleFor(x => x.treeName)
            .NotEmpty()
            .Length(1, 64)
            .WithMessage("максимальная длина имени дерева 64 символа");

        RuleFor(x => x.nodeName)
           .NotEmpty()
           .Length(1, 64)
           .WithMessage("максимальная длина имени узла дерева 64 символа");
    }
}
