using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.FamilyMembers.Commands.UpdateFamilyMember;

public sealed record UpdateFamilyMemberCommand(
    long Id,
    long MemberId,
    string? Dni,
    string? FirstName,
    string? LastName,
    string? Relationship,
    DateOnly? BirthDate,
    bool IsAuthorized,
    string UpdatedBy
) : ICommand<bool>;
