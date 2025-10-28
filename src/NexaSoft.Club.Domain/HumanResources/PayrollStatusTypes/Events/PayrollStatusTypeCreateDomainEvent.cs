using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollStatusTypes.Events;

public sealed record PayrollStatusTypeCreateDomainEvent
(
    long Id
): IDomainEvent;
