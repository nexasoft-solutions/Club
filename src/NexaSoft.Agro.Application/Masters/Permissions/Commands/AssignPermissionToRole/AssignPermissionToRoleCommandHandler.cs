using FluentValidation;
using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Exceptions;
using NexaSoft.Agro.Application.Masters.Roles;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Permissions;
using NexaSoft.Agro.Domain.Masters.Roles;

namespace NexaSoft.Agro.Application.Masters.Permissions.Commands.AssignPermissionToRole;

public class AssignPermissionToRoleCommandHandler(
    IGenericRepository<Role> _roleRepository,
    IRolePermissionRepository _rolePermissionRepository,
    IUnitOfWork _unitOfWork,
    ILogger<AssignPermissionToRoleCommandHandler> _logger,
    IValidator<AssignPermissionToRoleCommand> _validator) : ICommandHandler<AssignPermissionToRoleCommand, bool>
{
    public async Task<Result<bool>> Handle(AssignPermissionToRoleCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando asignación de permisos al Rol: {RoleId}", command.RoleId);

        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => new ValidationError(e.PropertyName, e.ErrorMessage));
            throw new ValidationExceptions(errors);
        }

        if (!await _roleRepository.ExistsAsync(command.RoleId, cancellationToken))
        {
            _logger.LogWarning("Rol no encontrado: {RoleId}", command.RoleId);
            return Result.Failure<bool>(RoleErrores.NoEncontrado);
        }

        // 2. Obtener y eliminar relaciones existentes
        var total = await _rolePermissionRepository.AddRangeAsync(
            command.RoleId,
            command.PermissionIds,
            cancellationToken);

        if (total == 0)
        {
            _logger.LogWarning("No se encontraron permisos para revocar");
            return Result.Failure<bool>(PermissionErrores.PermisosNoEncontradosEnRol);
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("Permisos asignados correctamente al Rol {RoleId}", command.RoleId);
            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al asignar permisos al rol.");
            return Result.Failure<bool>(RoleErrores.ErrorAsignarPermiso);
        }
    }
}
