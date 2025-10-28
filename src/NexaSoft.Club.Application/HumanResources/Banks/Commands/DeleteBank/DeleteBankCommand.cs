using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.Banks.Commands.DeleteBank;

public sealed record DeleteBankCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
