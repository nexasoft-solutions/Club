
using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.MemberVisits.Commands.ExitVisitMember;

public sealed record  ExitVisitMemberCommand
(
    long MemberId,
    string CheckOutBy,
    string? Notes
) : ICommand<bool>;