using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollFormulas.Events;

public sealed record PayrollFormulaCreateDomainEvent
(
    long Id
): IDomainEvent;
