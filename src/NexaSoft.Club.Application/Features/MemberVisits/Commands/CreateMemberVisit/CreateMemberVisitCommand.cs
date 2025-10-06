using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.MemberVisits.Commands.CreateMemberVisit;

public sealed record CreateMemberVisitCommand(
    long MemberId,
    string? QrCodeUsed,
    string? Notes,
    string CreatedBy
) : ICommand<long>;
