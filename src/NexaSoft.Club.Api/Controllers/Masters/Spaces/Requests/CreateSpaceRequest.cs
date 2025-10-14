namespace NexaSoft.Club.Api.Controllers.Masters.Spaces.Request;

public sealed record CreateSpaceRequest(
    string SpaceName,
    long SpaceTypeId,
    int? Capacity,
    string? Description,
    decimal StandardRate,
    bool RequiresApproval,
    int MaxReservationHours,
    long? IncomeAccountId,
    string CreatedBy
);
