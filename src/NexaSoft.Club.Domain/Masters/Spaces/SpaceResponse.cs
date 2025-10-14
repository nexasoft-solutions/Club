namespace NexaSoft.Club.Domain.Masters.Spaces;

public sealed record SpaceResponse(
    long Id,
    long SpaceTypeId,
    string? SpaceTypeName,
    string? SpaceName,
    int? Capacity,
    string? Description,
    decimal StandardRate,
    bool RequiresApproval,
    int MaxReservationHours,
    long? IncomeAccountId,
    string? AccountName,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
