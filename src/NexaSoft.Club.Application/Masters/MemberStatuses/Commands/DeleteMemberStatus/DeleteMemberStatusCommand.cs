using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.MemberStatuses.Commands.DeleteMemberStatus;

public sealed record DeleteMemberStatusCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
