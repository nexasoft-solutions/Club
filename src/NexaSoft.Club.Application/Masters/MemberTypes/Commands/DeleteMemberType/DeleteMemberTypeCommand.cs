using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.MemberTypes.Commands.DeleteMemberType;

public sealed record DeleteMemberTypeCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
