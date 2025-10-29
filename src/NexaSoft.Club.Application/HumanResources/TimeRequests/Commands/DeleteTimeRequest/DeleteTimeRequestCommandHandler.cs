using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.TimeRequests;

namespace NexaSoft.Club.Application.HumanResources.TimeRequests.Commands.DeleteTimeRequest;

public class DeleteTimeRequestCommandHandler(
    IGenericRepository<TimeRequest> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteTimeRequestCommandHandler> _logger
) : ICommandHandler<DeleteTimeRequestCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteTimeRequestCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de TimeRequest con ID {TimeRequestId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("TimeRequest con ID {TimeRequestId} no encontrado", command.Id);
                return Result.Failure<bool>(TimeRequestErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar TimeRequest con ID {TimeRequestId}", command.Id);
            return Result.Failure<bool>(TimeRequestErrores.ErrorDelete);
        }
    }
}
