using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.Masters.Spaces;

namespace NexaSoft.Club.Domain.Masters.SpaceAvailabilities;

public class SpaceAvailability : Entity
{
    public long SpaceId { get; private set; }
    public Space? Space { get; private set; }
    public int DayOfWeek { get; private set; }
    public TimeSpan StartTime { get; private set; }
    public TimeSpan EndTime { get; private set; }
    /*public int MinReservationHours { get; private set; }
    public int MaxReservationDaysInAdvance { get; private set; }
    public int? MaxReservationsPerDay { get; private set; }*/
    public int StateSpaceAvailability { get; private set; }

    private SpaceAvailability() { }

    private SpaceAvailability(
        long spaceId, 
        int dayOfWeek, 
        TimeSpan startTime, 
        TimeSpan endTime,         
        int stateSpaceAvailability, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        SpaceId = spaceId;
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        EndTime = endTime;   
        StateSpaceAvailability = stateSpaceAvailability;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static SpaceAvailability Create(
        long spaceId, 
        int dayOfWeek, 
        TimeSpan startTime, 
        TimeSpan endTime,    
        int stateSpaceAvailability, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new SpaceAvailability(
            spaceId,
            dayOfWeek,
            startTime,
            endTime,     
            stateSpaceAvailability,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        long spaceId, 
        int dayOfWeek, 
        TimeSpan startTime, 
        TimeSpan endTime,   
        DateTime utcNow,
        string? updatedBy
    )
    {
        SpaceId = spaceId;
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        EndTime = endTime;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateSpaceAvailability = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
