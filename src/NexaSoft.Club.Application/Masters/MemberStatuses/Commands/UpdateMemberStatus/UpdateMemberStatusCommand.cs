using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.MemberStatuses.Commands.UpdateMemberStatus;

public sealed record UpdateMemberStatusCommand(
    long Id,
    string? StatusName,
    string? Description,
    bool CanAccess,
    bool CanReserve,
    bool CanParticipate,
    string UpdatedBy
) : ICommand<bool>;
