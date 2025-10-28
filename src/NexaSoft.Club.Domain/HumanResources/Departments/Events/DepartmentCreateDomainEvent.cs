using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.Departments.Events;

public sealed record DepartmentCreateDomainEvent
(
    long Id
): IDomainEvent;
