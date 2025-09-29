using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Masters.AccountingCharts;

namespace NexaSoft.Club.Application.Masters.AccountingCharts.Queries.GetAccountingCharts;

public sealed record GetAccountingChartsQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<AccountingChartResponse>>;
