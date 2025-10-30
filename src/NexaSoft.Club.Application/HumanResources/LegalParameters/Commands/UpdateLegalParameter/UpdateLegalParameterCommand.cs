using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.LegalParameters.Commands.UpdateLegalParameter;

public sealed record UpdateLegalParameterCommand(
    long Id,
    string? Code,
    string? Name,
    decimal Value,
    string? ValueText,
    DateOnly EffectiveDate,
    DateOnly? EndDate,
    string? Category,
    string? Description,
    string UpdatedBy
) : ICommand<bool>;
