using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.Companies.Commands.CreateCompany;

public sealed record CreateCompanyCommand(
    string? Ruc,
    string? BusinessName,
    string? TradeName,
    string? Address,
    string? Phone,
    string? Website,
    string CreatedBy
) : ICommand<long>;
