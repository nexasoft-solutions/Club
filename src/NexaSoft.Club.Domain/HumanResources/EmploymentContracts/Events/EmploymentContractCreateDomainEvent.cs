using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.EmploymentContracts.Events;

public sealed record EmploymentContractCreateDomainEvent
(
    long Id
): IDomainEvent;
