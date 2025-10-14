using NexaSoft.Club.Application.Features.Payments.Commands.CreatePayment;

namespace NexaSoft.Club.Api.Controllers.Features.Payments.Request;

public sealed record CreatePaymentRequest(
    long MemberId,
    List<PaymentItemRequest>? PaymentItems,
    decimal Amount,
    DateOnly PaymentDate,
    long PaymentMethodId,
    string? ReferenceNumber,
    long DocumentTypeId,
    string? ReceiptNumber,   
    string CreatedBy
);

public record PaymentItemRequest
(
    long MemberFeeId,
    decimal AmountToPay
);