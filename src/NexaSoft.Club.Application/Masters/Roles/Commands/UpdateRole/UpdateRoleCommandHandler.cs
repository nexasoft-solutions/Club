
using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Roles;

namespace NexaSoft.Club.Application.Masters.Roles.Commands.CreateRole;

public class UpdateRoleCommandHandler(IGenericRepository<Role> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateRoleCommandHandler> _logger) : ICommandHandler<UpdateRoleCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de Rol con ID {RoleId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Rol con ID {RoleId} no encontrado", command.Id);
            return Result.Failure<bool>(RoleErrores.NoEncontrado);
        }

        entity.Update(
             command.Name,
             command.Description,
             _dateTimeProvider.CurrentTime.ToUniversalTime(),
             command.UsuarioModificacion
         );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Constante con ID {RoleId} actualizado satisfactoriamente", entity.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear Rol");
            return Result.Failure<bool>(RoleErrores.ErrorEdit);
        }
    }
}
