
using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Roles;

namespace NexaSoft.Club.Application.Masters.Roles.Commands.DeleteRole;

public class DeleteRoleCommandHandler(IGenericRepository<Role> _repository,
    IUnitOfWork _unitOfWork,
    ILogger<DeleteRoleCommandHandler> _logger) : ICommandHandler<DeleteRoleCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de eliminación de Rol con ID {RoleId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Rol con ID {RolId} no encontrado", command.Id);
            return Result.Failure<bool>(RoleErrores.NoEncontrado);
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
            _logger.LogError(ex, "Error al eliminar Rol con ID {RolId}", command.Id);
            return Result.Failure<bool>(RoleErrores.ErrorDelete);
        }
    }
}
