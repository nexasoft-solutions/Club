using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Responsables.Events;

public sealed record ResponsableUpdateDomainEvent
(
    long Id
): IDomainEvent;
