using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollConceptDepartments.Events;

public sealed record PayrollConceptDepartmentUpdateDomainEvent
(
    long Id
): IDomainEvent;
