using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Features.Reservations;

namespace NexaSoft.Club.Application.Features.Reservations.Queries.ReservationByWeek;

public sealed record GetReservationByWeekQuery
(
    int Year,
    int WeekNumber,
    long SpaceId
): IQuery<List<ReservationWeekSummaryResponse>>;
