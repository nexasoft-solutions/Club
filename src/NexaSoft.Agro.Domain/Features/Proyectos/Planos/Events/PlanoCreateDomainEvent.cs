using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Planos.Events;

public sealed record PlanoCreateDomainEvent
(
    Guid Id
): IDomainEvent;
