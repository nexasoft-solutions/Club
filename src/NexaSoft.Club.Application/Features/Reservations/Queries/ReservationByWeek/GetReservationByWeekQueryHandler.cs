using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.Reservations;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Application.Features.Reservations.Queries.ReservationByWeek;

public class GetReservationByWeekQueryHandler(
    IGenericRepository<Reservation> _repository
) : IQueryHandler<GetReservationByWeekQuery, List<ReservationWeekSummaryResponse>>
{
    public async Task<Result<List<ReservationWeekSummaryResponse>>> Handle(GetReservationByWeekQuery query, CancellationToken cancellationToken)
    {

        try
        {

            var spec = new ReservationSummaryByWeekSpec(query.Year, query.WeekNumber, query.SpaceId);
            var reservations = await _repository.ListAsync(spec, cancellationToken);
            if (reservations == null || !reservations.Any())
            {
                return Result.Failure<List<ReservationWeekSummaryResponse>>(ReservationErrores.NoExisteReservaEnlaSemana);
            }


            return Result.Success(reservations.ToList());

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<List<ReservationWeekSummaryResponse>>(ReservationErrores.ErrorConsultaReservaPorFecha);
        }
    }
}
