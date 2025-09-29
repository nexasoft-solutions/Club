
using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Permissions;
using NexaSoft.Club.Domain.Masters.Roles;

namespace NexaSoft.Club.Application.Masters.Permissions.Commands.DeletePermission;

public class DeletePermissionCommandHandler(IGenericRepository<Role> _repository,
    IUnitOfWork _unitOfWork,
    ILogger<DeletePermissionCommandHandler> _logger) : ICommandHandler<DeletePermissionCommand, bool>
{
    public async Task<Result<bool>> Handle(DeletePermissionCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de eliminación de Permiso con ID {PermisoId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Permiso con ID {PermisoId} no encontrado", command.Id);
            return Result.Failure<bool>(PermissionErrores.NoEncontrado);
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al eliminar Rol con ID {PermisoId}", command.Id);
            return Result.Failure<bool>(PermissionErrores.ErrorDelete);
        }
    }
}
