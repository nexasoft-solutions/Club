namespace NexaSoft.Club.Api.Controllers.Masters.UserTypes.Request;

public sealed record UpdateUserTypeRequest(
   long Id,
    string? Name,
    string? Description,
    bool IsAdministrative,
    string UpdatedBy
);
