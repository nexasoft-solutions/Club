using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Masters.Roles;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Roles;

namespace NexaSoft.Agro.Application.Masters.Permissions.Commands.ClearRolePermissions;

public class ClearRolePermissionsCommandHandler(
    IGenericRepository<Role> _roleRepository,
    IRolePermissionRepository _rolePermissionRepository,
    IUnitOfWork _unitOfWork,
    ILogger<ClearRolePermissionsCommandHandler> _logger)
    : ICommandHandler<ClearRolePermissionsCommand, bool>
{
    public async Task<Result<bool>> Handle(ClearRolePermissionsCommand command, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(command.RoleId, cancellationToken);
        if (role == null)
            return Result.Failure<bool>(RoleErrores.NoEncontrado);

        await _rolePermissionRepository.ClearForRoleAsync(command.RoleId, cancellationToken);

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("Todos los permisos fueron removidos del rol {RoleId}", role.Id);
            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al limpiar permisos del rol");
            return Result.Failure<bool>(RoleErrores.ErrorLimpiarPermisos);
        }
    }
}
