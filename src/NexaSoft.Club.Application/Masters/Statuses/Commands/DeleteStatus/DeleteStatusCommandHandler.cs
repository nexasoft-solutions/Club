using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Statuses;

namespace NexaSoft.Club.Application.Masters.Statuses.Commands.DeleteStatus;

public class DeleteStatusCommandHandler(
    IGenericRepository<Status> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteStatusCommandHandler> _logger
) : ICommandHandler<DeleteStatusCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteStatusCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de Status con ID {StatusId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("Status con ID {StatusId} no encontrado", command.Id);
                return Result.Failure<bool>(StatusErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar Status con ID {StatusId}", command.Id);
            return Result.Failure<bool>(StatusErrores.ErrorDelete);
        }
    }
}
