using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.SystemUsers;

namespace NexaSoft.Club.Application.Masters.SystemUsers.Commands.DeleteSystemUser;

public class DeleteSystemUserCommandHandler(
    IGenericRepository<SystemUser> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteSystemUserCommandHandler> _logger
) : ICommandHandler<DeleteSystemUserCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteSystemUserCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de SystemUser con ID {SystemUserId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("SystemUser con ID {SystemUserId} no encontrado", command.Id);
                return Result.Failure<bool>(SystemUserErrores.NoEncontrado);
            }

         entity.Delete(_dateTimeProvider.CurrentTime.ToUniversalTime(),command.DeletedBy);

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex,"Error al eliminar SystemUser con ID {SystemUserId}", command.Id);
            return Result.Failure<bool>(SystemUserErrores.ErrorDelete);
        }
    }
}
