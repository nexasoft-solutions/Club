using FluentValidation;

namespace NexaSoft.Club.Application.Masters.SystemUsers.Commands.CreateSystemUser;

public class CreateSystemUserCommandValidator : AbstractValidator<CreateSystemUserCommand>
{
    public CreateSystemUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("El campo Username no puede estar vacío.");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El campo Email no puede estar vacío.");
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("El campo FirstName no puede estar vacío.");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("El campo LastName no puede estar vacío.");
        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("El campo Role no puede estar vacío.");
    }
}
