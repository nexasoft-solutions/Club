using FluentValidation;

namespace NexaSoft.Agro.Application.Masters.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.UserApellidos)
            .NotEmpty().WithMessage("El campo UserApellidos no puede estar vacío.");
        RuleFor(x => x.UserNombres)
            .NotEmpty().WithMessage("El campo UserNombres no puede estar vacío.");     
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("El campo Password no puede estar vacío.");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El campo Email no puede estar vacío.");
        RuleFor(x => x.UserDni)
            .NotEmpty().WithMessage("El campo UserDni no puede estar vacío.");
        RuleFor(x => x.UserTelefono)
            .NotEmpty().WithMessage("El campo UserTelefono no puede estar vacío.");
    }
}
