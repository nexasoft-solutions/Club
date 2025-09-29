using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.SpaceRates;

namespace NexaSoft.Club.Application.Masters.SpaceRates.Commands.DeleteSpaceRate;

public class DeleteSpaceRateCommandHandler(
    IGenericRepository<SpaceRate> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteSpaceRateCommandHandler> _logger
) : ICommandHandler<DeleteSpaceRateCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteSpaceRateCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de SpaceRate con ID {SpaceRateId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("SpaceRate con ID {SpaceRateId} no encontrado", command.Id);
                return Result.Failure<bool>(SpaceRateErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar SpaceRate con ID {SpaceRateId}", command.Id);
            return Result.Failure<bool>(SpaceRateErrores.ErrorDelete);
        }
    }
}
