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
    string? UpdatedBy,
    List<SpacePhotosResponse> SpacePhotos,
    List<SpaceAvailabilitysResponse> SpaceAvailabilities
);


public class SpacePhotosResponse
{
    public long Id { get; set; }
    public long SpaceId { get; set; }
    public string? PhotoUrl { get; set; }
    public int Order { get; set; }
    public string? Description { get; set; }

    public SpacePhotosResponse(long id, long spaceId, string? photoUrl, int order, string? description)
    {
        Id = id;
        SpaceId = spaceId;
        PhotoUrl = photoUrl;
        Order = order;
        Description = description;
    }
}


public sealed record SpaceAvailabilitysResponse
(
    long Id,
    long SpaceId,
    int DayOfWeek,
    TimeSpan StartTime,
    TimeSpan EndTime
);



