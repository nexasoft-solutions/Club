using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollFormulas.Events;

public sealed record PayrollFormulaUpdateDomainEvent
(
    long Id
): IDomainEvent;
