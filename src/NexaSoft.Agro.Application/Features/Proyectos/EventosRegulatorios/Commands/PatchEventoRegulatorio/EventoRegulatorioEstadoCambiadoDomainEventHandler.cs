using MediatR;
using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.Cumplimientos;
using NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios;
using NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios.Events;

namespace NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Commands.PatchEventoRegulatorio;

public class EventoRegulatorioEstadoCambiadoDomainEventHandler(
    IGenericRepository<CumplimientoSeguimiento> _cumplimientoRepository,
    IGenericRepository<EventoRegulatorio> _eventoRepository,
    IDateTimeProvider _dateTimeProvider,
    ILogger<EventoRegulatorioEstadoCambiadoDomainEventHandler> _logger
) : INotificationHandler<EventoRegulatorioEstadoCambiadoDomainEvent>
{
    public async Task Handle(EventoRegulatorioEstadoCambiadoDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Generando Seguimiento de Cumplimiento por cambio de estado de EventoRegulatorio ID {Id}", notification.EventoRegulatorioId);

        var evento = await _eventoRepository.GetByIdAsync(notification.EventoRegulatorioId, cancellationToken);
        if (evento is null)
        {
            _logger.LogWarning("EventoRegulatorio con ID {Id} no encontrado", notification.EventoRegulatorioId);
             Result.Failure<bool>(EventoRegulatorioErrores.NoEncontrado);
        }

        var seguimiento = CumplimientoSeguimiento.Create(
            notification.EventoRegulatorioId,
            notification.EstadoAnteriorId,
            notification.EstadoNuevoId,
            notification.Observaciones,
            DateOnly.FromDateTime(_dateTimeProvider.CurrentTime.ToUniversalTime()),
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            notification.UsuarioCambio
        );

        await _cumplimientoRepository.AddAsync(seguimiento, cancellationToken);
       
    }
}
