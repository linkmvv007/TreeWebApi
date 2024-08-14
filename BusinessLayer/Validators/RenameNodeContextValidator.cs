using BusinessLayer.Commands;
using FluentValidation;

namespace BusinessLayer.Validators;

public class RenameNodeContextValidator : AbstractValidator<RenameNodeContext>
{
    public RenameNodeContextValidator()
    {
        RuleFor(x => x.nodeId).NotNull();

        RuleFor(x => x.treeName)
           .NotNull()
           .Length(1, 64)
           .WithMessage("максимальная длина имени дерева 64 символа");

        RuleFor(x => x.newNodeName)
           .NotNull()
           .Length(1, 64)
           .WithMessage("максимальная длина имени узла дерева 64 символа");
        //.When(x =>
        //{
        //    var debugNode = x;
        //    return true;
        //}
        //);

    }
}
