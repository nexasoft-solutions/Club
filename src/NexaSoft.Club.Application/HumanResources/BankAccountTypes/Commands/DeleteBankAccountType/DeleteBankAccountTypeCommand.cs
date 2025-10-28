using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.BankAccountTypes.Commands.DeleteBankAccountType;

public sealed record DeleteBankAccountTypeCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
