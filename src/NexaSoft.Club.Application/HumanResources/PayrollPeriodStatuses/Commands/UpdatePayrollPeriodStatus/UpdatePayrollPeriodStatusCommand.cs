using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriodStatuses.Commands.UpdatePayrollPeriodStatus;

public sealed record UpdatePayrollPeriodStatusCommand(
    long Id,
    string? Code,
    string? Name,
    string? Description,
    string UpdatedBy
) : ICommand<bool>;
