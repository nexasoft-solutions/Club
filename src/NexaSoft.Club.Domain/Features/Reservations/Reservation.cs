using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Features.AccountingEntries;
using NexaSoft.Club.Domain.Masters.Statuses;
using NexaSoft.Club.Domain.Masters.PaymentTypes;
using NexaSoft.Club.Domain.Masters.DocumentTypes;
using NexaSoft.Club.Domain.Masters.SpaceAvailabilities;
using NexaSoft.Club.Domain.Masters.Spaces;

namespace NexaSoft.Club.Domain.Features.Reservations;

public class Reservation : Entity
{
    public long MemberId { get; private set; }
    public Member? Member { get; private set; }
    public long SpaceId { get; private set; }
    public Space? Space { get; private set; }

    public long SpaceAvailabilityId { get; private set; }
    public SpaceAvailability? SpaceAvailability { get; private set; }
    public DateOnly Date { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public long? StatusId { get; private set; }
    public Status? Status { get; private set; }
    public long PaymentMethodId { get; private set; }
    public PaymentType? PaymentType { get; private set; }
    public string? ReferenceNumber { get; private set; }
    public long DocumentTypeId { get; private set; }
    public DocumentType? DocumentType { get; private set; }
    public string? ReceiptNumber { get; private set; }
    public decimal TotalAmount { get; private set; }
    public long? AccountingEntryId { get; private set; }
    public AccountingEntry? AccountingEntry { get; private set; }
    public int StateReservation { get; private set; }
    public int Year { get; private set; }
    public int WeekNumber { get; private set; }
    private Reservation() { }

    private Reservation(
        long memberId,
        long spaceId,
        long spaceAvailabilityId,
        DateOnly date,
        TimeOnly startTime,
        TimeOnly endTime,
        long? statusId,
        long paymentMethodId,
        string? referenceNumber,
        long documentTypeId,
        string? receiptNumber,
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
        SpaceAvailabilityId = spaceAvailabilityId;
        StartTime = startTime;
        Date = date;
        EndTime = endTime;
        StatusId = statusId;
        PaymentMethodId = paymentMethodId;
        ReferenceNumber = referenceNumber;
        DocumentTypeId = documentTypeId;
        ReceiptNumber = receiptNumber;
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
        long spaceAvailabilityId,
        DateOnly date,
        TimeOnly startTime,
        TimeOnly endTime,
        long? statusId,
        long paymentMethodId,
        string? referenceNumber,
        long documentTypeId,
        string? receiptNumber,
        decimal totalAmount,
        long? accountingEntryId,
        int stateReservation,
        DateTime createdAt,
        string? createdBy
    )
    {
        // Calcular año y número de semana basado en date
        var dateTime = date.ToDateTime(new TimeOnly(0, 0));
        var calendar = System.Globalization.CultureInfo.InvariantCulture.Calendar;
        var weekNumber = calendar.GetWeekOfYear(dateTime, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        var year = dateTime.Year;

        var entity = new Reservation(
            memberId,
            spaceId,
            spaceAvailabilityId,
            date,
            startTime,
            endTime,
            statusId,
            paymentMethodId,
            referenceNumber,
            documentTypeId,
            receiptNumber,
            totalAmount,
            accountingEntryId,
            stateReservation,
            createdAt,
            createdBy
        )
        {
            Year = year,
            WeekNumber = weekNumber
        };

        return entity;
    }


    public Result Delete(DateTime utcNow, string deletedBy)
    {
        StateReservation = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }

    public void SetAccountingEntryId(long accountingEntryId)
    {
        AccountingEntryId = accountingEntryId;
        UpdatedAt = DateTime.UtcNow;
    }


    public void MarkAsCompleted(long accountingEntryId)
    {
        SetAccountingEntryId(accountingEntryId);
        StatusId = (long)StatusEnum.Completado;
        UpdatedAt = DateTime.UtcNow;
        // Puedes agregar más lógica de estado si es necesario
    }

    public void MarkAsFailed()
    {
        StatusId = (long)StatusEnum.Fallido;
        //ErrorMessage = error;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsProcessing()
    {
        StatusId = (long)StatusEnum.Iniciado;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsActive()
    {
        StatusId = (long)StatusEnum.Activo;
        UpdatedAt = DateTime.UtcNow;
    }
}
