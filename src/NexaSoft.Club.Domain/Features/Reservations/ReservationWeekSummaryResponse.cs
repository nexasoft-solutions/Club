namespace NexaSoft.Club.Domain.Features.Reservations;

public sealed record ReservationWeekSummaryResponse
(
    DateOnly Date,
    int DayOfWeek,          // 0 = Domingo, 1 = Lunes, ..., 6 = Sábado
    TimeOnly StartTime,
    TimeOnly EndTime
);
