using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.EmployeesInfo.Events;

public sealed record EmployeeInfoUpdateDomainEvent
(
    long Id
): IDomainEvent;
