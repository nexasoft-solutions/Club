using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollConfigs.Commands.CreatePayrollConfig;

public sealed record CreatePayrollConfigCommand(
    long? CompanyId,
    long? PayPeriodTypeId,
    decimal RegularHoursPerDay,
    int WorkDaysPerWeek,
    string CreatedBy
) : ICommand<long>;
