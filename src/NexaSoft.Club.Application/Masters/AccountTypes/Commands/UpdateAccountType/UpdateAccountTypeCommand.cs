using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.AccountTypes.Commands.UpdateAccountType;

public sealed record UpdateAccountTypeCommand(
    long Id,
    string? Name,
    string? Description,
    string UpdatedBy
) : ICommand<bool>;
