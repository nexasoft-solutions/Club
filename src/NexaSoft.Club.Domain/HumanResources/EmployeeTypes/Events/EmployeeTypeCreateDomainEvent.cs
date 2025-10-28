using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.EmployeeTypes.Events;

public sealed record EmployeeTypeCreateDomainEvent
(
    long Id
): IDomainEvent;
