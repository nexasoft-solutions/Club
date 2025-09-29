using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.FeeConfigurations.Events;

public sealed record FeeConfigurationCreateDomainEvent
(
    long Id
): IDomainEvent;
