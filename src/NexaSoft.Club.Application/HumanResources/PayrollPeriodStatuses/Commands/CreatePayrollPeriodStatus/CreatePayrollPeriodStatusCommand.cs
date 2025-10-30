using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriodStatuses.Commands.CreatePayrollPeriodStatus;

public sealed record CreatePayrollPeriodStatusCommand(
    string? Code,
    string? Name,
    string? Description,
    string CreatedBy
) : ICommand<long>;
