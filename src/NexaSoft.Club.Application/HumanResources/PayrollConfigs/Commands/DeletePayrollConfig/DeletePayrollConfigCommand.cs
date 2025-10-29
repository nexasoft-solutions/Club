using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollConfigs.Commands.DeletePayrollConfig;

public sealed record DeletePayrollConfigCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
