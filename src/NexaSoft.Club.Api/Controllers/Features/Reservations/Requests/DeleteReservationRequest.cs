namespace NexaSoft.Club.Api.Controllers.Features.Reservations.Request;

public sealed record DeleteReservationRequest(
   long Id,
   string DeletedBy
);
