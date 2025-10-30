using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriodStatuses;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriodStatuses.Queries.GetPayrollPeriodStatus;

public sealed record GetPayrollPeriodStatusQuery(
    long Id
) : IQuery<PayrollPeriodStatusResponse>;
