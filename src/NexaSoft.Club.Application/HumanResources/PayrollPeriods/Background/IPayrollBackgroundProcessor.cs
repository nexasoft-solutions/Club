using NexaSoft.Club.Application.HumanResources.PayrollPeriods.Commands.CreatePayrollPeriod;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriods.Background;

public interface IPayrollBackgroundProcessor
{
    Task ProcessPayrollAsync(long payrollPeriodId, CreatePayrollPeriodCommand command, string periodName, CancellationToken cancellationToken);
}
