using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriods;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriods.Queries.GetPayrollPeriodByEmployee;

public sealed record GetPayrollPeriodByEmployeeQuery
(
    long EmployeeId
):IQuery<List<PayrollPeriodItemResponse>>;
