using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.ContractTypes.Events;

public sealed record ContractTypeUpdateDomainEvent
(
    long Id
): IDomainEvent;
