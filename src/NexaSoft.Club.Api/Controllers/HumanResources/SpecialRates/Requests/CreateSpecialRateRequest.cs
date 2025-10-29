namespace NexaSoft.Club.Api.Controllers.HumanResources.SpecialRates.Request;

public sealed record CreateSpecialRateRequest(
    long? RateTypeId,
    string? Name,
    decimal Multiplier,
    TimeOnly? StartTime,
    TimeOnly? EndTime,
    string CreatedBy
);
