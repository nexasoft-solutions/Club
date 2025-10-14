using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Features.Reservations;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Features.Reservations.Queries.GetReservation;

public class GetReservationQueryHandler(
    IGenericRepository<Reservation> _repository
) : IQueryHandler<GetReservationQuery, ReservationResponse>
{
    public async Task<Result<ReservationResponse>> Handle(GetReservationQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new ReservationSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
            if (entity is null)
                return Result.Failure<ReservationResponse>(ReservationErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<ReservationResponse>(ReservationErrores.ErrorConsulta);
        }
    }
}
