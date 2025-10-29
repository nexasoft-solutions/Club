namespace NexaSoft.Club.Api.Controllers.HumanResources.RateTypes.Request;

public sealed record UpdateRateTypeRequest(
   long Id,
    string? Code,
    string? Name,
    string? Description,
    string UpdatedBy
);
