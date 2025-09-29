using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Periodicities;

namespace NexaSoft.Club.Application.Masters.Periodicities.Commands.DeletePeriodicity;

public class DeletePeriodicityCommandHandler(
    IGenericRepository<Periodicity> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeletePeriodicityCommandHandler> _logger
) : ICommandHandler<DeletePeriodicityCommand, bool>
{
    public async Task<Result<bool>> Handle(DeletePeriodicityCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de eliminación de Periodicity con ID {PeriodicityId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Periodicity con ID {PeriodicityId} no encontrado", command.Id);
            return Result.Failure<bool>(PeriodicityErrores.NoEncontrado);
        }

        entity.Delete(_dateTimeProvider.CurrentTime.ToUniversalTime(), command.DeletedBy);

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
            _logger.LogError(ex, "Error al eliminar Periodicity con ID {PeriodicityId}", command.Id);
            return Result.Failure<bool>(PeriodicityErrores.ErrorDelete);
        }
    }
}
