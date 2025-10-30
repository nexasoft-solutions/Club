using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployeeTypes.Events;

public sealed record PayrollConceptEmployeeTypeUpdateDomainEvent
(
    long Id
): IDomainEvent;
