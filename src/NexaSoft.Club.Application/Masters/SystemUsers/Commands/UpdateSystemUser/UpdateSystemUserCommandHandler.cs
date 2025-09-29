using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.SystemUsers;

namespace NexaSoft.Club.Application.Masters.SystemUsers.Commands.UpdateSystemUser;

public class UpdateSystemUserCommandHandler(
    IGenericRepository<SystemUser> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateSystemUserCommandHandler> _logger
) : ICommandHandler<UpdateSystemUserCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateSystemUserCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de actualización de SystemUser con ID {SystemUserId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("SystemUser con ID {SystemUserId} no encontrado", command.Id);
                return Result.Failure<bool>(SystemUserErrores.NoEncontrado);
            }

        entity.Update(
            command.Id,
            command.Username,
            command.Email,
            command.FirstName,
            command.LastName,
            command.Role,
            command.IsActive,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("SystemUser con ID {SystemUserId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al actualizar SystemUser con ID {SystemUserId}", command.Id);
            return Result.Failure<bool>(SystemUserErrores.ErrorEdit);
        }
    }
}
