using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.SpaceAvailabilities;

namespace NexaSoft.Club.Application.Masters.SpaceAvailabilities.Commands.UpdateSpaceAvailability;

public class UpdateSpaceAvailabilityCommandHandler(
    IGenericRepository<SpaceAvailability> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateSpaceAvailabilityCommandHandler> _logger
) : ICommandHandler<UpdateSpaceAvailabilityCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateSpaceAvailabilityCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de SpaceAvailability con ID {SpaceAvailabilityId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("SpaceAvailability con ID {SpaceAvailabilityId} no encontrado", command.Id);
            return Result.Failure<bool>(SpaceAvailabilityErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.SpaceId,
            command.DayOfWeek,
            command.StartTime,
            command.EndTime,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UpdatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("SpaceAvailability con ID {SpaceAvailabilityId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar SpaceAvailability con ID {SpaceAvailabilityId}", command.Id);
            return Result.Failure<bool>(SpaceAvailabilityErrores.ErrorEdit);
        }
    }
}
