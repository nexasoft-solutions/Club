using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios.Events;

public sealed record EventoRegulatorioEstadoCambiadoDomainEvent(
    long EventoRegulatorioId,
    int EstadoNuevoId,
    int EstadoAnteriorId,
    string Observaciones,
    DateTime FechaActualizacion,
    string UsuarioCambio
): IDomainEvent;

