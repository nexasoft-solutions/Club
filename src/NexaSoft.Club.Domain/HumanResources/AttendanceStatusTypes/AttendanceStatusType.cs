using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.HumanResources.AttendanceStatusTypes;

public class AttendanceStatusType : Entity
{
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public bool? IsPaid { get; private set; }
    public string? Description { get; private set; }
    public int StateAttendanceStatusType { get; private set; }

    private AttendanceStatusType() { }

    private AttendanceStatusType(
        string? code, 
        string? name, 
        bool? isPaid, 
        string? description, 
        int stateAttendanceStatusType, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Code = code;
        Name = name;
        IsPaid = isPaid;
        Description = description;
        StateAttendanceStatusType = stateAttendanceStatusType;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static AttendanceStatusType Create(
        string? code, 
        string? name, 
        bool? isPaid, 
        string? description, 
        int stateAttendanceStatusType, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new AttendanceStatusType(
            code,
            name,
            isPaid,
            description,
            stateAttendanceStatusType,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? code, 
        string? name, 
        bool? isPaid, 
        string? description, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        Code = code;
        Name = name;
        IsPaid = isPaid;
        Description = description;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateAttendanceStatusType = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
