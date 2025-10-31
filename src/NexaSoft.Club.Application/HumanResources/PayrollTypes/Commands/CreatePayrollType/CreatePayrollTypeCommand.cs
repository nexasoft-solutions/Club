using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollTypes.Commands.CreatePayrollType;

public sealed record CreatePayrollTypeCommand(
    string? Code,
    string? Name,
    string? Description,
    string CreatedBy
) : ICommand<long>;
