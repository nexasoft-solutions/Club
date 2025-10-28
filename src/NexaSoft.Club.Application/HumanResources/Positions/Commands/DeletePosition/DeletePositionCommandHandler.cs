using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.Positions;

namespace NexaSoft.Club.Application.HumanResources.Positions.Commands.DeletePosition;

public class DeletePositionCommandHandler(
    IGenericRepository<Position> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeletePositionCommandHandler> _logger
) : ICommandHandler<DeletePositionCommand, bool>
{
    public async Task<Result<bool>> Handle(DeletePositionCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de Position con ID {PositionId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("Position con ID {PositionId} no encontrado", command.Id);
                return Result.Failure<bool>(PositionErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar Position con ID {PositionId}", command.Id);
            return Result.Failure<bool>(PositionErrores.ErrorDelete);
        }
    }
}
