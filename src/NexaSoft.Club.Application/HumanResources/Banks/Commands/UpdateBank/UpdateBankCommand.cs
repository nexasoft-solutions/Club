using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.Banks.Commands.UpdateBank;

public sealed record UpdateBankCommand(
    long Id,
    string? Code,
    string? Name,
    string? WebSite,
    string? Phone,
    string UpdatedBy
) : ICommand<bool>;
