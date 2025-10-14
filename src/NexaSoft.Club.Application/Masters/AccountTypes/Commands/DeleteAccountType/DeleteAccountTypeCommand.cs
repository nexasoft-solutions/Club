using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.AccountTypes.Commands.DeleteAccountType;

public sealed record DeleteAccountTypeCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
