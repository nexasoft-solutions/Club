using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.MemberVisits.Commands.UpdateMemberVisit;

public sealed record UpdateMemberVisitCommand(
    long Id,
    long? MemberId,
    DateOnly? VisitDate,
    TimeOnly? EntryTime,
    TimeOnly? ExitTime,
    string? QrCodeUsed,
    string? Notes,
    string? CheckInBy,
    string? CheckOutBy,
    int VisitType,
    string UpdatedBy
) : ICommand<bool>;
