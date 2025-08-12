using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Application.Exceptions;
using NexaSoft.Agro.Application.Storages;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.Archivos;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Application.Features.Proyectos.Archivos.Commands.CreateArchivo;

public class CreateArchivoCommandHandler(
    IGenericRepository<Archivo> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<CreateArchivoCommandHandler> _logger,
    IFileStorageService _fileStorageService
) : ICommandHandler<CreateArchivoCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateArchivoCommand command, CancellationToken cancellationToken)
    {

        _logger.LogInformation("Iniciando proceso de creación de Archivo");
        var validator = new CreateArchivoCommandValidator();
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
               .Select(failure => new ValidationError(failure.PropertyName, failure.ErrorMessage));

            throw new ValidationExceptions(errors);
        }

        var extension = Path.GetExtension(command.NombreArchivo); 
        var nuevoNombre = $"archivo_{Guid.NewGuid()}{extension}";

        string rutaArchivo = await _fileStorageService.UploadAsync(
           command.ArchivoStream,
           nuevoNombre,
           command.ArchivoTipo,
           cancellationToken
        );


        var entity = Archivo.Create(
            command.NombreArchivo,
            command.DescripcionArchivo, 
            nuevoNombre,
            DateOnly.FromDateTime(_dateTimeProvider.CurrentTime.ToUniversalTime()),
            command.TipoArchivo,
            command.SubCapituloId,
            command.EstructuraId,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime()
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Archivo con ID {ArchivoId} creado satisfactoriamente", entity.Id);

            return Result.Success(entity.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al crear Archivo");
            return Result.Failure<Guid>(ArchivoErrores.ErrorSave);
        }
    }
}
