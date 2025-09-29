using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.FamilyMembers.Commands.DeleteFamilyMember;

public sealed record DeleteFamilyMemberCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
