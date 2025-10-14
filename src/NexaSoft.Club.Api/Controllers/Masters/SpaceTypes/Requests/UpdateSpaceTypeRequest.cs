namespace NexaSoft.Club.Api.Controllers.Masters.SpaceTypes.Request;

public sealed record UpdateSpaceTypeRequest(
   long Id,
    string? Name,
    string? Description,
    string UpdatedBy
);
