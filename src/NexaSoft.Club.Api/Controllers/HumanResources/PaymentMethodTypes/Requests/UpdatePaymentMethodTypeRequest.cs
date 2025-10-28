namespace NexaSoft.Club.Api.Controllers.HumanResources.PaymentMethodTypes.Request;

public sealed record UpdatePaymentMethodTypeRequest(
   long Id,
    string? Code,
    string? Name,
    string? Description,
    string UpdatedBy
);
