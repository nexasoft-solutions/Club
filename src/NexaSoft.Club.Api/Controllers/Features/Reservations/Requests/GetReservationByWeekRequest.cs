namespace NexaSoft.Club.Api.Controllers.Features.Reservations.Requests;

public sealed record GetReservationByWeekRequest
(
    int Year,
    int WeekNumber,
    long SpaceId
);
