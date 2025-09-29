namespace NexaSoft.Club.Api.Controllers.Masters.SystemUsers.Request;

public sealed record CreateSystemUserRequest(
    string? Username,
    string? Email,
    string? FirstName,
    string? LastName,
    string? Role,
    bool IsActive,
    string CreatedBy
);
