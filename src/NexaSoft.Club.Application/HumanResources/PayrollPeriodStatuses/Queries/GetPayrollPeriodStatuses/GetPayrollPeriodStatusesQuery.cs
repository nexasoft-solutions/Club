using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriodStatuses;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriodStatuses.Queries.GetPayrollPeriodStatuses;

public sealed record GetPayrollPeriodStatusesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<PayrollPeriodStatusResponse>>;
