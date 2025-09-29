using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.Payments.Events;

public sealed record PaymentCreateDomainEvent
(
    long PaymentId,
    long MemberId,
    decimal Amount,
    string PaymentMethod,
    string ReceiptNumber,
    DateOnly PaymentDate,
    List<PaymentItemDetail> PaymentItems,
    string CreatedBy,
    DateTime CreatedOn
) : IDomainEvent;

public record PaymentItemDetail(
    long MemberFeeId,
    decimal AmountToPay
);