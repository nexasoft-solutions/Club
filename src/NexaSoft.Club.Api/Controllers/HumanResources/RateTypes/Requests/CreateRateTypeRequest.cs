namespace NexaSoft.Club.Api.Controllers.HumanResources.RateTypes.Request;

public sealed record CreateRateTypeRequest(
    string? Code,
    string? Name,
    string? Description,
    string CreatedBy
);
