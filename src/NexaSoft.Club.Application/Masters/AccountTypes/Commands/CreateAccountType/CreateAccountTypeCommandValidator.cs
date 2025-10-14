using FluentValidation;

namespace NexaSoft.Club.Application.Masters.AccountTypes.Commands.CreateAccountType;

public class CreateAccountTypeCommandValidator : AbstractValidator<CreateAccountTypeCommand>
{
    public CreateAccountTypeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo Name no puede estar vacío.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
    }
}
