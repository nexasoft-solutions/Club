using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.MemberVisits.Commands.DeleteMemberVisit;

public sealed record DeleteMemberVisitCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
