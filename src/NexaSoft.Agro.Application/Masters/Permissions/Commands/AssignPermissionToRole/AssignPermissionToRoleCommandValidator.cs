using System;
using FluentValidation;

namespace NexaSoft.Agro.Application.Masters.Permissions.Commands.AssignPermissionToRole;

public class AssignPermissionToRoleCommandValidator : AbstractValidator<AssignPermissionToRoleCommand>
{
    public AssignPermissionToRoleCommandValidator()
    {
        RuleFor(c => c.RoleId).NotEmpty().WithMessage("El ID del rol es obligatorio.");
        RuleFor(c => c.PermissionIds)
            .NotEmpty().WithMessage("Debe asignar al menos un permiso.")
            .Must(ids => ids.All(id => id != Guid.Empty))
            .WithMessage("Todos los IDs de permisos deben ser válidos.");
    }

}
