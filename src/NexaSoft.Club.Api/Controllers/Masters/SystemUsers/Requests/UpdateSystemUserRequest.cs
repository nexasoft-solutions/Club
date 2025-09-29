namespace NexaSoft.Club.Api.Controllers.Masters.SystemUsers.Request;

public sealed record UpdateSystemUserRequest(
   long Id,
    string? Username,
    string? Email,
    string? FirstName,
    string? LastName,
    string? Role,
    bool IsActive,
    string UpdatedBy
);
