namespace NexaSoft.Club.Api.Controllers.HumanResources.SpecialRates.Request;

public sealed record UpdateSpecialRateRequest(
   long Id,
    long? RateTypeId,
    string? Name,
    decimal Multiplier,
    TimeOnly? StartTime,
    TimeOnly? EndTime,
    string UpdatedBy
);
