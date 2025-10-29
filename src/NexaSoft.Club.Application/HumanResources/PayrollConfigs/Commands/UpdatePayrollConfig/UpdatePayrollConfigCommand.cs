using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollConfigs.Commands.UpdatePayrollConfig;

public sealed record UpdatePayrollConfigCommand(
    long Id,
    long? CompanyId,
    long? PayPeriodTypeId,
    decimal RegularHoursPerDay,
    int WorkDaysPerWeek,
    string UpdatedBy
) : ICommand<bool>;
