using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.Members.Commands.DeleteMember;

public sealed record DeleteMemberCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
