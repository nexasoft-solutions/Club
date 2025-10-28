using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.BankAccountTypes.Commands.CreateBankAccountType;

public sealed record CreateBankAccountTypeCommand(
    string? Code,
    string? Name,
    string? Description,
    string CreatedBy
) : ICommand<long>;
