using FluentValidation;

namespace NexaSoft.Agro.Application.Masters.Permissions.Commands.CreatePermision;

public class CreatePermissionCommandValidator: AbstractValidator<CreatePermissionCommand>
{
    public CreatePermissionCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo Nombre no puede estar vacío.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Descripción no puede estar vacío.");
    }

}
