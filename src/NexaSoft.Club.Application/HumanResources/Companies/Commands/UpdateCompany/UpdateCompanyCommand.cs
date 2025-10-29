using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.Companies.Commands.UpdateCompany;

public sealed record UpdateCompanyCommand(
    long Id,
    string? Ruc,
    string? BusinessName,
    string? TradeName,
    string? Address,
    string? Phone,
    string? Website,
    string UpdatedBy
) : ICommand<bool>;
