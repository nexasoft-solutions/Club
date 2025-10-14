using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.Masters.Spaces;

namespace NexaSoft.Club.Domain.Masters.SpacePhotos;

public class SpacePhoto : Entity
{
    public long SpaceId { get; private set; }
    public Space? Space { get; private set; }
    public string? PhotoUrl { get; private set; }
    public int Order { get; private set; }
    public string? Description { get; private set; }
    public int StateSpacePhoto { get; private set; }

    private SpacePhoto() { }

    private SpacePhoto(
        long spaceId, 
        string? photoUrl, 
        int order, 
        string? description, 
        int stateSpacePhoto, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        SpaceId = spaceId;
        PhotoUrl = photoUrl;
        Order = order;
        Description = description;
        StateSpacePhoto = stateSpacePhoto;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static SpacePhoto Create(
        long spaceId, 
        string? photoUrl, 
        int order, 
        string? description, 
        int stateSpacePhoto, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new SpacePhoto(
            spaceId,
            photoUrl,
            order,
            description,
            stateSpacePhoto,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        long spaceId, 
        string? photoUrl, 
        int order, 
        string? description, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        SpaceId = spaceId;
        PhotoUrl = photoUrl;
        Order = order;
        Description = description;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateSpacePhoto = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
