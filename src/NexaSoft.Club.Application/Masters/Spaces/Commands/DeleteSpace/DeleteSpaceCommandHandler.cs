using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Spaces;

namespace NexaSoft.Club.Application.Masters.Spaces.Commands.DeleteSpace;

public class DeleteSpaceCommandHandler(
    IGenericRepository<Space> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteSpaceCommandHandler> _logger
) : ICommandHandler<DeleteSpaceCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteSpaceCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de Space con ID {SpaceId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("Space con ID {SpaceId} no encontrado", command.Id);
                return Result.Failure<bool>(SpaceErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar Space con ID {SpaceId}", command.Id);
            return Result.Failure<bool>(SpaceErrores.ErrorDelete);
        }
    }
}
