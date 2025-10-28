using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.Departments.Events;

public sealed record DepartmentUpdateDomainEvent
(
    long Id
): IDomainEvent;
