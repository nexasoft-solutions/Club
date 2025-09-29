using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Periodicities;

namespace NexaSoft.Club.Application.Masters.Periodicities.Commands.UpdatePeriodicity;

public class UpdatePeriodicityCommandHandler(
    IGenericRepository<Periodicity> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdatePeriodicityCommandHandler> _logger
) : ICommandHandler<UpdatePeriodicityCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdatePeriodicityCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de Periodicity con ID {PeriodicityId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Periodicity con ID {PeriodicityId} no encontrado", command.Id);
            return Result.Failure<bool>(PeriodicityErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.Name,
            command.Description,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Periodicity con ID {PeriodicityId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar Periodicity con ID {PeriodicityId}", command.Id);
            return Result.Failure<bool>(PeriodicityErrores.ErrorEdit);
        }
    }
}
