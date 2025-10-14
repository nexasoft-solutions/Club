using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.SpacePhotos;

namespace NexaSoft.Club.Application.Masters.SpacePhotos.Commands.UpdateSpacePhoto;

public class UpdateSpacePhotoCommandHandler(
    IGenericRepository<SpacePhoto> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateSpacePhotoCommandHandler> _logger
) : ICommandHandler<UpdateSpacePhotoCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateSpacePhotoCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de SpacePhoto con ID {SpacePhotoId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("SpacePhoto con ID {SpacePhotoId} no encontrado", command.Id);
            return Result.Failure<bool>(SpacePhotoErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.SpaceId,
            command.PhotoUrl,
            command.Order,
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
            _logger.LogInformation("SpacePhoto con ID {SpacePhotoId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar SpacePhoto con ID {SpacePhotoId}", command.Id);
            return Result.Failure<bool>(SpacePhotoErrores.ErrorEdit);
        }
    }
}
