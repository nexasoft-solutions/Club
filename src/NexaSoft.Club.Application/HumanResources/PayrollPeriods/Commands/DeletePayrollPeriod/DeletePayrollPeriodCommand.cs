using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriods.Commands.DeletePayrollPeriod;

public sealed record DeletePayrollPeriodCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
