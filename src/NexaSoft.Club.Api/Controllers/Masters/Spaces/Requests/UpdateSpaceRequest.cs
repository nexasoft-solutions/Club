namespace NexaSoft.Club.Api.Controllers.Masters.Spaces.Request;

public sealed record UpdateSpaceRequest(
    long Id,
    string SpaceName,
    long SpaceTypeId,
    int Capacity,
    string? Description,
    decimal StandardRate,
    bool RequiresApproval,
    int MaxReservationHours,
    long? IncomeAccountId,
    string UpdatedBy
);
