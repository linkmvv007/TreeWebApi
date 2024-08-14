using BusinessLayer.RequestContext;
using FluentValidation;

namespace BusinessLayer.Validators;

public class RenameNodeContextValidator : AbstractValidator<RenameNodeContext>
{
    public RenameNodeContextValidator()
    {
        RuleFor(x => x.nodeId)
            .NotEmpty();

        RuleFor(x => x.treeName)
           .NotEmpty()
           .Length(1, 64)
           .WithMessage(MsgValidation.ErrorMaxLength64);

        RuleFor(x => x.newNodeName)
           .NotEmpty()
           .Length(1, 64)
           .WithMessage(MsgValidation.ErrorMaxLength64);
        //.When(x =>
        //{
        //    var debugNode = x;
        //    return true;
        //}
        //);

    }
}
