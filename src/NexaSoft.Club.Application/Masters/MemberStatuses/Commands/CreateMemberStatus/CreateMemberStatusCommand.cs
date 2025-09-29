using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.MemberStatuses.Commands.CreateMemberStatus;

public sealed record CreateMemberStatusCommand(
    string? StatusName,
    string? Description,
    bool CanAccess,
    bool CanReserve,
    bool CanParticipate,
    string CreatedBy
) : ICommand<long>;
