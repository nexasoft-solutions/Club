using System;
using NexaSoft.Club.Application.HumanResources.PayrollPeriods.Commands.CreatePayrollPeriod;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriods.Background;

public interface IPayrollBackgroundTaskService
{
    Task QueuePayrollProcessingAsync(long payrollPeriodId, CreatePayrollPeriodCommand command, string periodName, CancellationToken cancellationToken = default);
}
