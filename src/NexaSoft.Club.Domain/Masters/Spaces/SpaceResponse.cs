namespace NexaSoft.Club.Domain.Masters.Spaces;

public sealed record SpaceResponse(
    long Id,
    string? SpaceName,
    string? SpaceType,
    int? Capacity,
    string? Description,
    decimal StandardRate,
    bool IsActive,
    bool RequiresApproval,
    int MaxReservationHours,
    long? IncomeAccountId,
    string? AccountName,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
