using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Estructuras.Events;

public sealed record EstructuraUpdateDomainEvent
(
    Guid Id
): IDomainEvent;
