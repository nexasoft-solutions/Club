using MediatR;
using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.Cumplimientos;
using NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios;
using NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios.Events;

namespace NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Commands.CreateEventoRegulatorio;

public class EventoRegulatorioCreadoDomainEventHandler(
    IGenericRepository<CumplimientoSeguimiento> _cumplimientoRepository,
    IGenericRepository<EventoRegulatorio> _eventoRepository,
    IDateTimeProvider _dateTimeProvider,
    ILogger<EventoRegulatorioCreadoDomainEvent> _logger
) : INotificationHandler<EventoRegulatorioCreadoDomainEvent>
{
    public async Task Handle(EventoRegulatorioCreadoDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de creación de Seguimiento de Cumplimiento  con ID {EventoRegulatorioId}", notification.Id);

        var entity = await _eventoRepository.GetByIdAsync(notification.Id, cancellationToken);
        if (entity == null)
        {

            _logger.LogWarning("EventoRegulatorio con ID {EventoRegulatorioId} no encontrado", notification.Id);
            Result.Failure<bool>(EventoRegulatorioErrores.NoEncontrado);
        }

        var seguimiento = CumplimientoSeguimiento.Create(
            notification.Id,
            null,
            entity!.EstadoEventoId, // estadoNuevo
            "Evento creado y estado inicial 'Programado'",
            DateOnly.FromDateTime(_dateTimeProvider.CurrentTime.ToUniversalTime()),
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            entity.UsuarioCreacion
        );

        await _cumplimientoRepository.AddAsync(seguimiento, cancellationToken);
        _logger.LogInformation("Seguimiento de Cumplimiento con ID {EventoRegulatorioId} creado satisfactoriamente", entity.Id);
    }
}
