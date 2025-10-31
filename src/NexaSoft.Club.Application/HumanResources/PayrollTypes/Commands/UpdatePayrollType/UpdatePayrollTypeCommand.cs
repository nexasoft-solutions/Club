using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollTypes.Commands.UpdatePayrollType;

public sealed record UpdatePayrollTypeCommand(
    long Id,
    string? Code,
    string? Name,
    string? Description,
    string UpdatedBy
) : ICommand<bool>;
