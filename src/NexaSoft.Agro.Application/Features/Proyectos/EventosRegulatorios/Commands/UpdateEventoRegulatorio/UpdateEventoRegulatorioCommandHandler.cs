using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios;

namespace NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Commands.UpdateEventoRegulatorio;

public class UpdateEventoRegulatorioCommandHandler(
    IGenericRepository<EventoRegulatorio> _repository,
    IEventoRegulatorioRepository _eventoRepository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<UpdateEventoRegulatorioCommandHandler> _logger
) : ICommandHandler<UpdateEventoRegulatorioCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateEventoRegulatorioCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de EventoRegulatorio con ID {EventoRegulatorioId}", command.Id);
        var entity = await _eventoRepository.GetByIdWithResponsablesAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("EventoRegulatorio con ID {EventoRegulatorioId} no encontrado", command.Id);
            return Result.Failure<bool>(EventoRegulatorioErrores.NoEncontrado);
        }

        entity.Update(
            command.Id,
            command.NombreEvento,
            command.TipoEventoId,
            command.FrecuenciaEventoId,
            command.FechaExpedición,
            command.FechaVencimiento,
            command.Descripcion,
            command.NotificarDíasAntes,
            command.ResponsableId,
            command.EstudioAmbientalId,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.UsuarioModificacion,
            command.ResponsablesAdicionales
        );

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            _logger.LogInformation("EventoRegulatorio con ID {EventoRegulatorioId} actualizado satisfactoriamente", command.Id);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error al actualizar EventoRegulatorio con ID {EventoRegulatorioId}", command.Id);
            return Result.Failure<bool>(EventoRegulatorioErrores.ErrorEdit);
        }
    }
}
