using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriods;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriods.Queries.GetPayrollPeriod;

public sealed record GetPayrollPeriodQuery(
    long Id
) : IQuery<PayrollPeriodResponse>;
