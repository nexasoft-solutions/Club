using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.EmploymentContracts.Events;

public sealed record EmploymentContractUpdateDomainEvent
(
    long Id
): IDomainEvent;
