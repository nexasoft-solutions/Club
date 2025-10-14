using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.SourceModules.Events;

public sealed record SourceModuleUpdateDomainEvent
(
    long Id
): IDomainEvent;
