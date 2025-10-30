using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployees.Events;

public sealed record PayrollConceptEmployeeUpdateDomainEvent
(
    long Id
): IDomainEvent;
