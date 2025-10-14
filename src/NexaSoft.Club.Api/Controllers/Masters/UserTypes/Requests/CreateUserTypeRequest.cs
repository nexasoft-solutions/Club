namespace NexaSoft.Club.Api.Controllers.Masters.UserTypes.Request;

public sealed record CreateUserTypeRequest(
    string? Name,
    string? Description,
    bool IsAdministrative,
    string CreatedBy
);
