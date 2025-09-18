using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios;
using NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios.Events;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Commands.PatchEventoRegulatorio;

public class PatchEventoRegulatorioCommandHandler(
    IGenericRepository<EventoRegulatorio> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<PatchEventoRegulatorioCommandHandler> _logger
) : ICommandHandler<PatchEventoRegulatorioCommand, bool>
{
    public async Task<Result<bool>> Handle(PatchEventoRegulatorioCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de actualización de EventoRegulatorio con ID {EventoRegulatorioId}", command.Id);

        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity is null)
        {
            _logger.LogWarning("EventoRegulatorio con ID {EventoRegulatorioId} no encontrado", command.Id);
            return Result.Failure<bool>(EventoRegulatorioErrores.NoEncontrado);
        }

        switch (entity.EstadoEventoId)
        {
            case (int)EstadosEventosEnum.Presentado:
            case (int)EstadosEventosEnum.Cancelado:
                _logger.LogWarning("El evento con ID {EventoRegulatorioId} no puede modificarse porque ya está en estado finalizado: {Estado}", entity.Id, entity.EstadoEventoId);
                return Result.Failure<bool>(EventoRegulatorioErrores.ErrorFlujoTerminado);

            case var estado when estado == command.NuevoEstado:
                _logger.LogWarning("El evento con ID {EventoRegulatorioId} ya se encuentra en el estado {Estado}", entity.Id, entity.EstadoEventoId);
                return Result.Failure<bool>(EventoRegulatorioErrores.ErrorEstadoEvento);
        }

        var estadoAnterior = entity.EstadoEventoId;

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            entity.UpdateStateEvent(
                _dateTimeProvider.CurrentTime.ToUniversalTime(),
                command.NuevoEstado,
                command.UsuarioModificacion,
                command.FechaVencimiento
            );

            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            entity.RaiseDomainEvent(new EventoRegulatorioEstadoCambiadoDomainEvent(
                          entity.Id,
                          command.NuevoEstado,
                          estadoAnterior,
                          command.Observaciones,
                          _dateTimeProvider.CurrentTime.ToUniversalTime(),
                          command.UsuarioModificacion
                      ));

            entity.RaiseDomainEvent(new EventoRegulatorioPatchDomainEvent(entity.Id));

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
