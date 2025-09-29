using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.Ubigeos.Events;

public sealed record UbigeoCreateDomainEvent
(
    long Id
): IDomainEvent;
