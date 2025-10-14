using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.SpaceAvailabilities;

namespace NexaSoft.Club.Application.Masters.SpaceAvailabilities.Commands.DeleteSpaceAvailability;

public class DeleteSpaceAvailabilityCommandHandler(
    IGenericRepository<SpaceAvailability> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteSpaceAvailabilityCommandHandler> _logger
) : ICommandHandler<DeleteSpaceAvailabilityCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteSpaceAvailabilityCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de SpaceAvailability con ID {SpaceAvailabilityId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("SpaceAvailability con ID {SpaceAvailabilityId} no encontrado", command.Id);
                return Result.Failure<bool>(SpaceAvailabilityErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar SpaceAvailability con ID {SpaceAvailabilityId}", command.Id);
            return Result.Failure<bool>(SpaceAvailabilityErrores.ErrorDelete);
        }
    }
}
