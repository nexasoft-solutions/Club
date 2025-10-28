namespace NexaSoft.Club.Domain.HumanResources.AttendanceStatusTypes;

public sealed record AttendanceStatusTypeResponse(
    long Id,
    string? Code,
    string? Name,
    bool? IsPaid,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
