using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Masters.Ubigeos.Events;

public sealed record UbigeoCreateDomainEvent
(
    Guid Id
): IDomainEvent;
