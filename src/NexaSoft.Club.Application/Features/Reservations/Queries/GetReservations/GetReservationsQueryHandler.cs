using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Features.Reservations;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Features.Reservations.Queries.GetReservations;

public class GetReservationsQueryHandler(
    IGenericRepository<Reservation> _repository
) : IQueryHandler<GetReservationsQuery, Pagination<ReservationResponse>>
{
    public async Task<Result<Pagination<ReservationResponse>>> Handle(GetReservationsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new ReservationSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<ReservationResponse>(spec, cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<ReservationResponse>(
                    query.SpecParams.PageIndex,
                    query.SpecParams.PageSize,
                    totalItems,
                    responses
              );

            return Result.Success(pagination);

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<Pagination<ReservationResponse>>(ReservationErrores.ErrorConsulta);
        }
    }
}
