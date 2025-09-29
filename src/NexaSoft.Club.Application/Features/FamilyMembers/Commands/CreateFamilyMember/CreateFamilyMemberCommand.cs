using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.FamilyMembers.Commands.CreateFamilyMember;

public sealed record CreateFamilyMemberCommand(
    long MemberId,
    string? Dni,
    string? FirstName,
    string? LastName,
    string? Relationship,
    DateOnly? BirthDate,
    bool IsAuthorized,
    string CreatedBy
) : ICommand<long>;
