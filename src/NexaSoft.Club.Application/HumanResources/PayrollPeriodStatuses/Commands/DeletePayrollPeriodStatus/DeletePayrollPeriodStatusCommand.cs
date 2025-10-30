using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriodStatuses.Commands.DeletePayrollPeriodStatus;

public sealed record DeletePayrollPeriodStatusCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
