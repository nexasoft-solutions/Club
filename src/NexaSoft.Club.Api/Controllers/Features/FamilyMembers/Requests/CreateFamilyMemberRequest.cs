namespace NexaSoft.Club.Api.Controllers.Features.FamilyMembers.Request;

public sealed record CreateFamilyMemberRequest(
    long MemberId,
    string? Dni,
    string? FirstName,
    string? LastName,
    string? Relationship,
    DateOnly? BirthDate,
    bool IsAuthorized,
    string CreatedBy
);
