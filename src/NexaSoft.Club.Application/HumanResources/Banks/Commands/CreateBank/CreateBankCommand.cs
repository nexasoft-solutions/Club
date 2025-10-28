using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.Banks.Commands.CreateBank;

public sealed record CreateBankCommand(
    string? Code,
    string? Name,
    string? WebSite,
    string? Phone,
    string CreatedBy
) : ICommand<long>;
