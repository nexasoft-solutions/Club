using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.Payments.Events;

public sealed record PaymentItemsCreatedDomainEvent(
    long PaymentId,
    long MemberId,
    decimal Amount,
    string PaymentMethod,
    string ReceiptNumber,
    List<AppliedPaymentItem> AppliedItems,
    string CreatedBy,
    DateTime CreatedOn
) : IDomainEvent;

/*public sealed record AppliedPaymentItem(
    long PaymentItemId,
    long MemberFeeId,
    decimal AmountApplied
);*/
