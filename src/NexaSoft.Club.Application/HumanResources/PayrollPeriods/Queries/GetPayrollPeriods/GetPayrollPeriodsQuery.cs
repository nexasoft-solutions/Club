using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriods;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriods.Queries.GetPayrollPeriods;

public sealed record GetPayrollPeriodsQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<PayrollPeriodResponse>>;
