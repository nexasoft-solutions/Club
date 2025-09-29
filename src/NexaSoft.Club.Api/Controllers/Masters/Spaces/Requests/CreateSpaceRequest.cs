namespace NexaSoft.Club.Api.Controllers.Masters.Spaces.Request;

public sealed record CreateSpaceRequest(
    string? SpaceName,
    string? SpaceType,
    int? Capacity,
    string? Description,
    decimal StandardRate,
    bool IsActive,
    bool RequiresApproval,
    int MaxReservationHours,
    long? IncomeAccountId,
    string CreatedBy
);
