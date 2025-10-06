using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.Features.Members;

namespace NexaSoft.Club.Domain.Features.MemberVisits;

public class MemberVisit : Entity
{
    public long? MemberId { get; private set; }
    public Member? Member { get; private set; }
    public DateOnly? VisitDate { get; private set; }
    public TimeOnly? EntryTime { get; private set; }
    public TimeOnly? ExitTime { get; private set; }
    public string? QrCodeUsed { get; private set; }
    public string? Notes { get; private set; }

    public string? CheckInBy { get; private set; }
    public string? CheckOutBy { get; private set; }
    public int VisitType { get; private set; }
    public int StateMemberVisit { get; private set; }

    private MemberVisit() { }

    private MemberVisit(
        long? memberId,
        DateOnly? visitDate,
        TimeOnly? entryTime,
        string? qrCodeUsed,
        string? notes,
        string? checkInBy,
        int visitType,
        int stateMemberVisit,
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        MemberId = memberId;
        VisitDate = visitDate;
        EntryTime = entryTime;
        QrCodeUsed = qrCodeUsed;
        Notes = notes;
        CheckInBy = checkInBy;
        VisitType = visitType;
        StateMemberVisit = stateMemberVisit;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static MemberVisit Create(
        long? memberId,
        DateOnly? visitDate,
        TimeOnly? entryTime,
        string? qrCodeUsed,
        string? notes,
        string? checkInBy,
        int visitType,
        int stateMemberVisit,
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new MemberVisit(
            memberId,
            visitDate,
            entryTime,
            qrCodeUsed,
            notes,
            checkInBy,
            visitType,
            stateMemberVisit,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        long? memberId,
        DateOnly? visitDate,
        TimeOnly? entryTime,
        TimeOnly? exitTime,
        string? qrCodeUsed,
        string? notes,
        string? checkInBy,
        string? checkOutBy,
        int visitType,
        DateTime utcNow,
        string? updatedBy
    )
    {
        MemberId = memberId;
        VisitDate = visitDate;
        EntryTime = entryTime;
        ExitTime = exitTime;
        QrCodeUsed = qrCodeUsed;
        Notes = notes;
        CheckInBy = checkInBy;
        CheckOutBy = checkOutBy;
        VisitType = visitType;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string deletedBy)
    {
        StateMemberVisit = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }

    public void RegisterExit(TimeOnly exitTime, string? checkOutBy = null, string? notes = null)
    {
        ExitTime = exitTime;
        CheckOutBy = checkOutBy;

        if (!string.IsNullOrEmpty(notes))
            Notes += $"\nSalida: {notes}";

        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = checkOutBy ?? "System";
    }

    public void UpdateNotes(string notes)
    {
        Notes = notes;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool IsActiveVisit() => ExitTime == null;

    public TimeSpan GetVisitDuration()
    {
        try
        {
            // Verificar que todas las propiedades necesarias tengan valor
            if (ExitTime == null || !VisitDate.HasValue || !EntryTime.HasValue)
                return TimeSpan.Zero;

            var visitDate = VisitDate.Value;
            var entryTime = EntryTime.Value;
            var exitTime = ExitTime.Value;

            var entryDateTime = new DateTime(visitDate.Year, visitDate.Month, visitDate.Day,
                                           entryTime.Hour, entryTime.Minute, entryTime.Second);

            var exitDateTime = new DateTime(visitDate.Year, visitDate.Month, visitDate.Day,
                                          exitTime.Hour, exitTime.Minute, exitTime.Second);

            if (exitDateTime < entryDateTime)
                exitDateTime = exitDateTime.AddDays(1);

            return exitDateTime - entryDateTime;
        }
        catch (Exception)
        {
            return TimeSpan.Zero;
        }
    }

    // Método helper para convertir DateOnly + TimeOnly a DateTime
    /*private DateTime CombineDateAndTime(DateOnly date, TimeOnly time)
    {
        return new DateTime(date.Year, date.Month, date.Day,
                          time.Hour, time.Minute, time.Second);
    }*/
}
