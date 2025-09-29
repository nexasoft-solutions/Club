using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.AccountingCharts.Events;

public sealed record AccountingChartUpdateDomainEvent
(
    long Id
): IDomainEvent;
