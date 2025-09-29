using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.FeeConfigurations.Events;

public sealed record FeeConfigurationUpdateDomainEvent
(
    long Id
): IDomainEvent;
