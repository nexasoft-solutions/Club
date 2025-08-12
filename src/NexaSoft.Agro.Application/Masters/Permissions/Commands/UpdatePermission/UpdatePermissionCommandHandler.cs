
using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Permissions;

namespace NexaSoft.Agro.Application.Masters.Permissions.Commands.CreatePermission;

public class UpdatePermissionCommandHandler(IGenericRepository<Permission> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdatePermissionCommandHandler> _logger) : ICommandHandler<UpdatePermissionCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdatePermissionCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de Permiso con ID {PermisoId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Permiso con ID {PermisoId} no encontrado", command.Id);
            return Result.Failure<bool>(PermissionErrores.NoEncontrado);
        }

        entity.Update(
             command.Name,
             command.Description,
             command.ReferenciaControl,
             _dateTimeProvider.CurrentTime.ToUniversalTime()
         );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Permiso con ID {PermisoId} actualizado satisfactoriamente", entity.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear Permiso");
            return Result.Failure<bool>(PermissionErrores.ErrorEdit);
        }
    }
}
