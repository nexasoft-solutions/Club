using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.SpaceAvailabilities;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Masters.SpaceAvailabilities.Commands.CreateSpaceAvailability;

public class CreateSpaceAvailabilityCommandHandler(
    IGenericRepository<SpaceAvailability> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateSpaceAvailabilityCommandHandler> _logger
) : ICommandHandler<CreateSpaceAvailabilityCommand, long>
{
    public async Task<Result<long>> Handle(CreateSpaceAvailabilityCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de SpaceAvailability");

        var entity = SpaceAvailability.Create(
            command.SpaceId,
            command.DayOfWeek,
            command.StartTime,
            command.EndTime,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.CreatedBy
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("SpaceAvailability con ID {SpaceAvailabilityId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear SpaceAvailability");
            return Result.Failure<long>(SpaceAvailabilityErrores.ErrorSave);
        }
    }
}
