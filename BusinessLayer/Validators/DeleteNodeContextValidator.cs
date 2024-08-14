using BusinessLayer.Commands;
using FluentValidation;

namespace BusinessLayer.Validators;

public class DeleteNodeContextValidator : AbstractValidator<DeleteNodeContext>
{
    public DeleteNodeContextValidator()
    {
        RuleFor(x => x.nodeId)
            .NotEmpty();

        RuleFor(x => x.treeName)
           .NotEmpty()
           .Length(1, 64)
           .WithMessage("максимальная длина имени дерева 64 символа");
    }
}
