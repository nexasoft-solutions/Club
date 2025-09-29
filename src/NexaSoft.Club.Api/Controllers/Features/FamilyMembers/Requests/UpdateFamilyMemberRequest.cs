namespace NexaSoft.Club.Api.Controllers.Features.FamilyMembers.Request;

public sealed record UpdateFamilyMemberRequest(
   long Id,
    long MemberId,
    string? Dni,
    string? FirstName,
    string? LastName,
    string? Relationship,
    DateOnly? BirthDate,
    bool IsAuthorized,
    string UpdatedBy
);
