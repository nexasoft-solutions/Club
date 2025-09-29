namespace NexaSoft.Club.Domain.Features.FamilyMembers;

public sealed record FamilyMemberResponse(
    long Id,
    long MemberId,
    string? MemberFirstName,
    string? MemberLastName,
    string? Dni,
    string? FirstName,
    string? LastName,
    string? Relationship,
    DateOnly? BirthDate,
    bool IsAuthorized,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
