namespace NexaSoft.Club.Domain.HumanResources.LegalParameters;

public sealed record LegalParameterResponse(
    long Id,
    string? Code,
    string? Name,
    decimal Value,
    string? ValueText,
    DateOnly EffectiveDate,
    DateOnly? EndDate,
    string? Category,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
