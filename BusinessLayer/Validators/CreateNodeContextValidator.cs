using BusinessLayer.RequestContext;
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
            .WithMessage(MsgValidation.ErrorMaxLength64);

        RuleFor(x => x.nodeName)
           .NotEmpty()
           .Length(1, 64)
           .WithMessage(MsgValidation.ErrorMaxLength64);
    }
}
