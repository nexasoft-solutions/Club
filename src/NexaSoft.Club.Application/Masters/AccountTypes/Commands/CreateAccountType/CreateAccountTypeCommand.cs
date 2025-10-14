using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.AccountTypes.Commands.CreateAccountType;

public sealed record CreateAccountTypeCommand(
    string? Name,
    string? Description,
    string CreatedBy
) : ICommand<long>;
