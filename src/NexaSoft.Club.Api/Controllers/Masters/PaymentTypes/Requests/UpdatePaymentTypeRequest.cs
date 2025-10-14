namespace NexaSoft.Club.Api.Controllers.Masters.PaymentTypes.Request;

public sealed record UpdatePaymentTypeRequest(
   long Id,
    string? Name,
    string? Description,
    string UpdatedBy
);
