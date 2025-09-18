using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Planos.Events;

public sealed record PlanoUpdateDomainEvent
(
    long Id
): IDomainEvent;
