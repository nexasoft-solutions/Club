namespace NexaSoft.Club.Api.Controllers.HumanResources.LegalParameters.Request;

public sealed record UpdateLegalParameterRequest(
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
);
