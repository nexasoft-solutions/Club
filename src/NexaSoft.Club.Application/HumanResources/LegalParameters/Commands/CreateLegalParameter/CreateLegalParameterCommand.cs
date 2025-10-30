using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.LegalParameters.Commands.CreateLegalParameter;

public sealed record CreateLegalParameterCommand(
    string? Code,
    string? Name,
    decimal Value,
    string? ValueText,
    DateOnly EffectiveDate,
    DateOnly? EndDate,
    string? Category,
    string? Description,
    string CreatedBy
) : ICommand<long>;
