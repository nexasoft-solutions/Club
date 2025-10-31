using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollTypes.Commands.DeletePayrollType;

public sealed record DeletePayrollTypeCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
