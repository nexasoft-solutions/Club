using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Features.Reservations;

namespace NexaSoft.Club.Application.Features.Reservations.Queries.GetReservation;

public sealed record GetReservationQuery(
    long Id
) : IQuery<ReservationResponse>;
