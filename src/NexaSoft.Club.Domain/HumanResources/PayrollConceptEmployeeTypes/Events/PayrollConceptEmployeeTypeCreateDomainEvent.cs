using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployeeTypes.Events;

public sealed record PayrollConceptEmployeeTypeCreateDomainEvent
(
    long Id
): IDomainEvent;
