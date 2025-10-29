namespace NexaSoft.Club.Domain.HumanResources.TimeRequests;

public sealed record TimeRequestResponse(
    long Id,
    long? EmployeeId,
    string? EmployeeCode,
    long? TimeRequestTypeId,
    string? TimeRequestTypeCode,
    DateOnly? StartDate,
    DateOnly? EndDate,
    int TotalDays,
    string? Reason,
    long? StatusId,
    string? StatusName,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
