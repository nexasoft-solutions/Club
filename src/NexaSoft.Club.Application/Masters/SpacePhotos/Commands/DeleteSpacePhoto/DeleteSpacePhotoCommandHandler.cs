using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.SpacePhotos;

namespace NexaSoft.Club.Application.Masters.SpacePhotos.Commands.DeleteSpacePhoto;

public class DeleteSpacePhotoCommandHandler(
    IGenericRepository<SpacePhoto> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<DeleteSpacePhotoCommandHandler> _logger
) : ICommandHandler<DeleteSpacePhotoCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteSpacePhotoCommand command, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Iniciando proceso de eliminación de SpacePhoto con ID {SpacePhotoId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (entity is null)
            {
            _logger.LogWarning("SpacePhoto con ID {SpacePhotoId} no encontrado", command.Id);
                return Result.Failure<bool>(SpacePhotoErrores.NoEncontrado);
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
            _logger.LogError(ex,"Error al eliminar SpacePhoto con ID {SpacePhotoId}", command.Id);
            return Result.Failure<bool>(SpacePhotoErrores.ErrorDelete);
        }
    }
}
