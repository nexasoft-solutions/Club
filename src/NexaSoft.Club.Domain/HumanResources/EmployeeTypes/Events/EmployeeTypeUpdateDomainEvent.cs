using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.EmployeeTypes.Events;

public sealed record EmployeeTypeUpdateDomainEvent
(
    long Id
): IDomainEvent;
