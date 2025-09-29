namespace NexaSoft.Club.Api.Controllers.Masters.Periodicities.Request;

public sealed record CreatePeriodicityRequest(
    string? Name,
    string? Description,
    string CreatedBy
);
