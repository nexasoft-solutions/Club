using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Masters.Spaces;
using NexaSoft.Club.Domain.Features.AccountingEntries;

namespace NexaSoft.Club.Domain.Features.Reservations;

public class Reservation : Entity
{
    public long MemberId { get; private set; }
    public Member? Member { get; private set; }
    public long SpaceId { get; private set; }
    public Space? Space { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public string? Status { get; private set; }
    public decimal TotalAmount { get; private set; }
    public long? AccountingEntryId { get; private set; }
    public AccountingEntry? AccountingEntry { get; private set; }
    public int StateReservation { get; private set; }

    private Reservation() { }

    private Reservation(
        long memberId, 
        long spaceId, 
        DateTime startTime, 
        DateTime endTime, 
        string? status, 
        decimal totalAmount, 
        long? accountingEntryId, 
        int stateReservation, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        MemberId = memberId;
        SpaceId = spaceId;
        StartTime = startTime;
        EndTime = endTime;
        Status = status;
        TotalAmount = totalAmount;
        AccountingEntryId = accountingEntryId;
        StateReservation = stateReservation;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static Reservation Create(
        long memberId, 
        long spaceId, 
        DateTime startTime, 
        DateTime endTime, 
        string? status, 
        decimal totalAmount, 
        long? accountingEntryId, 
        int stateReservation, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new Reservation(
            memberId,
            spaceId,
            startTime,
            endTime,
            status,
            totalAmount,
            accountingEntryId,
            stateReservation,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        long memberId, 
        long spaceId, 
        DateTime startTime, 
        DateTime endTime, 
        string? status, 
        decimal totalAmount, 
        long? accountingEntryId, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        MemberId = memberId;
        SpaceId = spaceId;
        StartTime = startTime;
        EndTime = endTime;
        Status = status;
        TotalAmount = totalAmount;
        AccountingEntryId = accountingEntryId;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateReservation = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
