using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Responsables.Events;

public sealed record ResponsableCreateDomainEvent
(
    long Id
): IDomainEvent;
