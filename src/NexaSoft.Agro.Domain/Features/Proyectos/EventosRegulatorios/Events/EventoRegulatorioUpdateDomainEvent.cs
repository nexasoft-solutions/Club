using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios.Events;

public sealed record EventoRegulatorioUpdateDomainEvent
(
    long Id
): IDomainEvent;
