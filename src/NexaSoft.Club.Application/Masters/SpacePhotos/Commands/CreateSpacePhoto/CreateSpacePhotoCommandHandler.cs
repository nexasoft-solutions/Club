using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Storages;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.SpacePhotos;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Masters.SpacePhotos.Commands.CreateSpacePhoto;

public class CreateSpacePhotoCommandHandler(
    IGenericRepository<SpacePhoto> _repository,
    ISpacePhotoStorageService _spacePhotoStorageService,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateSpacePhotoCommandHandler> _logger
) : ICommandHandler<CreateSpacePhotoCommand, long>
{
    public async Task<Result<long>> Handle(CreateSpacePhotoCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de SpacePhoto para SpaceId: {SpaceId}", command.SpaceId);

        // Validar que el PhotoFile no sea nulo
        if (command.PhotoFile == null)
        {
            _logger.LogWarning("Intento de crear SpacePhoto sin archivo para SpaceId: {SpaceId}", command.SpaceId);
            return Result.Failure<long>(SpacePhotoErrores.PhotoFileRequired);
        }

        try
        {
            // 1. Subir archivo a MinIO usando el Stream
            // NOTA: Necesitamos agregar los parámetros faltantes al command
            var photoUrl = await _spacePhotoStorageService.UploadSpacePhotoAsync(
                command.PhotoFile,
                command.OriginalFileName,    // Necesitamos agregar este campo
                command.ContentType,         // Necesitamos agregar este campo  
                command.SpaceId,
                cancellationToken
            );

            // 2. Crear la entidad con la URL generada por MinIO
            var entity = SpacePhoto.Create(
                command.SpaceId,
                photoUrl,                    // Usamos la URL generada, no command.PhotoUrl
                command.Order,
                command.Description ?? string.Empty,
                (int)EstadosEnum.Activo,
                _dateTimeProvider.CurrentTime.ToUniversalTime(),
                command.CreatedBy
            );

            // 3. Guardar en base de datos con transacción
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("SpacePhoto con ID {SpacePhotoId} creado satisfactoriamente para SpaceId: {SpaceId}", 
                entity.Id, command.SpaceId);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear SpacePhoto");
            return Result.Failure<long>(SpacePhotoErrores.ErrorSave);
        }
    }
}
