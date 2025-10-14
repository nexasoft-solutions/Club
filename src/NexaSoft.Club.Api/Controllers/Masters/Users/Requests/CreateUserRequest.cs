namespace NexaSoft.Club.Api.Controllers.Masters.Users.Request;

public sealed record CreateUserRequest(
    string? LastName,
    string? FirstName,
    string? Password,
    string? Email,
    string? Dni,
    string? Phone,
    long UserTypeId,
    long? MemberId,
    DateOnly BirthDate
);
