namespace NexaSoft.Club.Domain.HumanResources.SpecialRates;

public sealed record SpecialRateResponse(
    long Id,
    long? RateTypeId,
    string? RateTypeCode,
    string? Name,
    decimal Multiplier,
    TimeOnly? StartTime,
    TimeOnly? EndTime,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
