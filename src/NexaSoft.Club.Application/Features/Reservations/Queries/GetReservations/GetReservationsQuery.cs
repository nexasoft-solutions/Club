using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Features.Reservations;

namespace NexaSoft.Club.Application.Features.Reservations.Queries.GetReservations;

public sealed record GetReservationsQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<ReservationResponse>>;
