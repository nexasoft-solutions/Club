namespace NexaSoft.Club.Api.Controllers.HumanResources.PaymentMethodTypes.Request;

public sealed record CreatePaymentMethodTypeRequest(
    string? Code,
    string? Name,
    string? Description,
    string CreatedBy
);
