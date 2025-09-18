using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Cumplimientos.Events;

public sealed record CumplimientoUpdateDomainEvent
(
    long Id
): IDomainEvent;
