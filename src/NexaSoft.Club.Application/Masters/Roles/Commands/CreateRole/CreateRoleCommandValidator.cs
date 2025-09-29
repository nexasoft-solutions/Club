using System;
using FluentValidation;

namespace NexaSoft.Club.Application.Masters.Roles.Commands.CreateRole;

public class CreateRoleCommandValidator: AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo Nombre no puede estar vacío.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Descripción no puede estar vacío.");
    }

}
