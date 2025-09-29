namespace NexaSoft.Club.Api.Controllers.Masters.Spaces.Request;

public sealed record UpdateSpaceRequest(
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
    string UpdatedBy
);
