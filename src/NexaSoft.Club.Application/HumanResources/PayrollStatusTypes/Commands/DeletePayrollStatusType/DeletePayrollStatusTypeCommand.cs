using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollStatusTypes.Commands.DeletePayrollStatusType;

public sealed record DeletePayrollStatusTypeCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
