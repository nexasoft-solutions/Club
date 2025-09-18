using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Masters.Consultoras.Events;

public sealed record ConsultoraCreateDomainEvent
(
    long Id
): IDomainEvent;
