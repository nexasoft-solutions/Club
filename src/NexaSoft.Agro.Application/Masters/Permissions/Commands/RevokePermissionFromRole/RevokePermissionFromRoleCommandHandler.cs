using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Masters.Roles;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Permissions;
using NexaSoft.Agro.Domain.Masters.Roles;

namespace NexaSoft.Agro.Application.Masters.Permissions.Commands.RevokePermissionFromRole;

public class RevokePermissionFromRoleCommandHandler(
    IGenericRepository<Role> _roleRepository,
    IRolePermissionRepository _rolePermissionRepository,
    IUnitOfWork _unitOfWork,
    ILogger<RevokePermissionFromRoleCommandHandler> _logger) : ICommandHandler<RevokePermissionFromRoleCommand, bool>
{
    public async Task<Result<bool>> Handle(RevokePermissionFromRoleCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando revocación de asignación de permisos al Rol: {RoleId}", command.RoleId);

        if (!await _roleRepository.ExistsAsync(command.RoleId, cancellationToken))
        {
            _logger.LogWarning("Rol no encontrado: {RoleId}", command.RoleId);
            return Result.Failure<bool>(RoleErrores.NoEncontrado);
        }

        // 2. Obtener y eliminar relaciones existentes
        var total = await _rolePermissionRepository.RemoveRangeAsync(
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

            _logger.LogInformation("Permisos revocados correctamente del Rol {RoleId}", command.RoleId);
            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al revocar permisos al rol.");
            return Result.Failure<bool>(RoleErrores.ErrorRevocarPermiso);
        }
    }
}
