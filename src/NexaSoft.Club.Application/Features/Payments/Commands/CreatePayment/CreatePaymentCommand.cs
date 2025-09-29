using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.Payments.Commands.CreatePayment;

public sealed record CreatePaymentCommand(
    long MemberId,
    List<PaymentItemDto>? PaymentItems,
    decimal Amount,
    DateOnly PaymentDate,
    string? PaymentMethod,
    string? ReferenceNumber,
    string? ReceiptNumber,   
    string CreatedBy
) : ICommand<PaymentResponse>;


public sealed record PaymentItemDto
(
    long MemberFeeId,
    decimal AmountToPay
);

public sealed record PaymentResponse(
    long PaymentId,
    string ReceiptNumber,
    decimal Amount,
    DateOnly PaymentDate,
    string Status,
    List<PaymentItemResponse> AppliedItems
);

public record PaymentItemResponse(
    long PaymentItemId,
    long MemberFeeId,
    string Period,
    decimal AmountPaid,
    string FeeStatus
);

