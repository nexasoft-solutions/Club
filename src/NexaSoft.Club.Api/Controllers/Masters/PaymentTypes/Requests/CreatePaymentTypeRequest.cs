namespace NexaSoft.Club.Api.Controllers.Masters.PaymentTypes.Request;

public sealed record CreatePaymentTypeRequest(
    string? Name,
    string? Description,
    string CreatedBy
);
