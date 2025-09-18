using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales;

namespace NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Commands.UpdateEstudioAmbiental;

public class UpdateEstudioAmbientalCommandHandler(
    IGenericRepository<EstudioAmbiental> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateEstudioAmbientalCommandHandler> _logger
) : ICommandHandler<UpdateEstudioAmbientalCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateEstudioAmbientalCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de EstudioAmbiental con ID {EstudioAmbientalId}", command.Id);
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("EstudioAmbiental con ID {EstudioAmbientalId} no encontrado", command.Id);
            return Result.Failure<bool>(EstudioAmbientalErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.Proyecto,
            string.Empty,
            command.FechaInicio,
            command.FechaFin,
            command.Detalles,
            command.EmpresaId,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UsuarioModificacion
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("EstudioAmbiental con ID {EstudioAmbientalId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar EstudioAmbiental con ID {EstudioAmbientalId}", command.Id);
            return Result.Failure<bool>(EstudioAmbientalErrores.ErrorEdit);
        }
    }
}
