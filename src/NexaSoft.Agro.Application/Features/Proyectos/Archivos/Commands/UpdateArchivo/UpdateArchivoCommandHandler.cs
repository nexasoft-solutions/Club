using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.Archivos;

namespace NexaSoft.Agro.Application.Features.Proyectos.Archivos.Commands.UpdateArchivo;

public class UpdateArchivoCommandHandler(
    IGenericRepository<Archivo> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateArchivoCommandHandler> _logger
) : ICommandHandler<UpdateArchivoCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateArchivoCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de Archivo con ID {ArchivoId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("Archivo con ID {ArchivoId} no encontrado", command.Id);
            return Result.Failure<bool>(ArchivoErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.DescripcionArchivo,       
            _dateTimeProvider.CurrentTime.ToUniversalTime()
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("Archivo con ID {ArchivoId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar Archivo con ID {ArchivoId}", command.Id);
            return Result.Failure<bool>(ArchivoErrores.ErrorEdit);
        }
    }
}
