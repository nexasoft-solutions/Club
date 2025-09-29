namespace NexaSoft.Club.Api.Controllers.Features.Payments.Request;

public sealed record DeletePaymentRequest(
   long Id,
   string DeletedBy
);
