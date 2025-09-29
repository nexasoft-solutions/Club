namespace NexaSoft.Club.Api.Controllers.Masters.Periodicities.Request;

public sealed record UpdatePeriodicityRequest(
    long Id,
    string? Name,
    string? Description,
    string UpdatedBy
);
