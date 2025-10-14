namespace NexaSoft.Club.Api.Controllers.Masters.Statuses.Request;

public sealed record UpdateStatusRequest(
   long Id,
    string? Name,
    string? Description,
    string UpdatedBy
);
