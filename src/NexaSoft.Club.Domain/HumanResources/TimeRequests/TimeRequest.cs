using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;
using NexaSoft.Club.Domain.HumanResources.TimeRequestTypes;
using NexaSoft.Club.Domain.Masters.Statuses;

namespace NexaSoft.Club.Domain.HumanResources.TimeRequests;

public class TimeRequest : Entity
{
    public long? EmployeeId { get; private set; }
    public EmployeeInfo? EmployeeInfo { get; private set; }
    public long? TimeRequestTypeId { get; private set; }
    public TimeRequestType? TimeRequestType { get; private set; }
    public DateOnly? StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }
    public int TotalDays { get; private set; }
    public string? Reason { get; private set; }
    public long? StatusId { get; private set; }
    public Status? Status { get; private set; }
    public int StateTimeRequest { get; private set; }

    private TimeRequest() { }

    private TimeRequest(
        long? employeeId, 
        long? timeRequestTypeId, 
        DateOnly? startDate, 
        DateOnly? endDate, 
        int totalDays, 
        string? reason, 
        long? statusId, 
        int stateTimeRequest, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        EmployeeId = employeeId;
        TimeRequestTypeId = timeRequestTypeId;
        StartDate = startDate;
        EndDate = endDate;
        TotalDays = totalDays;
        Reason = reason;
        StatusId = statusId;
        StateTimeRequest = stateTimeRequest;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static TimeRequest Create(
        long? employeeId, 
        long? timeRequestTypeId, 
        DateOnly? startDate, 
        DateOnly? endDate, 
        int totalDays, 
        string? reason, 
        long? statusId, 
        int stateTimeRequest, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new TimeRequest(
            employeeId,
            timeRequestTypeId,
            startDate,
            endDate,
            totalDays,
            reason,
            statusId,
            stateTimeRequest,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        long? employeeId, 
        long? timeRequestTypeId, 
        DateOnly? startDate, 
        DateOnly? endDate, 
        int totalDays, 
        string reason, 
        long? statusId, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        EmployeeId = employeeId;
        TimeRequestTypeId = timeRequestTypeId;
        StartDate = startDate;
        EndDate = endDate;
        TotalDays = totalDays;
        Reason = reason;
        StatusId = statusId;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateTimeRequest = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
