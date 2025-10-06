namespace NexaSoft.Club.Api.Controllers.Features.MemberVisits.Request;

public sealed record UpdateMemberVisitRequest(
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
);
