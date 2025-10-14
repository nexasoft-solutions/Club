namespace NexaSoft.Club.Api.Controllers.Masters.SpaceTypes.Request;

public sealed record CreateSpaceTypeRequest(
    string? Name,
    string? Description,
    string CreatedBy
);
