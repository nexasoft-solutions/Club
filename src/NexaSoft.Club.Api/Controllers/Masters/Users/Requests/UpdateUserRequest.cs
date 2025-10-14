namespace NexaSoft.Club.Api.Controllers.Masters.Users.Request;

public sealed record UpdateUserRequest(
   long Id,
    string? LastName,
    string? FirstName,
    string? Email,
    string? Dni,
    string? Phone,
    long UserTypeId,
    DateOnly BirthDate,
    long? MemberId,
    string? UserModification
);
