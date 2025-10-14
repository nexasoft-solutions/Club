namespace NexaSoft.Club.Api.Controllers.Masters.Statuses.Request;

public sealed record CreateStatusRequest(
    string? Name,
    string? Description,
    string CreatedBy
);
