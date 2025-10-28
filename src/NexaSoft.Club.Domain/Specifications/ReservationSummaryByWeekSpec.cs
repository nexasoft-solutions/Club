using NexaSoft.Club.Domain.Features.Reservations;

namespace NexaSoft.Club.Domain.Specifications;

public class ReservationSummaryByWeekSpec: BaseSpecification<Reservation, ReservationWeekSummaryResponse>
{
    public ReservationSummaryByWeekSpec(int year, int weekNumber, long spaceId)
        : base(r =>
            r.Year == year &&
            r.WeekNumber == weekNumber &&
            r.SpaceId == spaceId)
    {
        AddInclude(r => r.Space!); // Para acceder a SpaceId

        AddSelect(r => new ReservationWeekSummaryResponse(
            r.Date,
            (int)r.Date.DayOfWeek, // Día de la semana como número (0 = Domingo)
            r.StartTime,
            r.EndTime
        ));

        AddOrderBy(r => r.Date);
        AddOrderBy(r => r.StartTime);
    }
}
