namespace NexaSoft.Club.Api.Controllers.HumanResources.PaymentMethodTypes.Request;

public sealed record DeletePaymentMethodTypeRequest(
   long Id,
   string DeletedBy
);
