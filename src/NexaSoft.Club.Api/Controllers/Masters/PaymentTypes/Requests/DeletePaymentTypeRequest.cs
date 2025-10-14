namespace NexaSoft.Club.Api.Controllers.Masters.PaymentTypes.Request;

public sealed record DeletePaymentTypeRequest(
   long Id,
   string DeletedBy
);
