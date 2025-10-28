using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.EmployeesInfo.Events;

public sealed record EmployeeInfoCreateDomainEvent
(
    long Id
): IDomainEvent;
