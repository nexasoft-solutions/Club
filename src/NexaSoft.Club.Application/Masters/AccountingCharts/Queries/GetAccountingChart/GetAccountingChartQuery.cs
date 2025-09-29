using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.AccountingCharts;

namespace NexaSoft.Club.Application.Masters.AccountingCharts.Queries.GetAccountingChart;

public sealed record GetAccountingChartQuery(
    long Id
) : IQuery<AccountingChartResponse>;
