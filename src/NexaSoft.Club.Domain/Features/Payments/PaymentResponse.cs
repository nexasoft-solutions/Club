namespace NexaSoft.Club.Domain.Features.Payments;

public sealed record PaymentResponse(
    long Id,
    long MemberId,
    string? MemberFirstName,
    string? MemberLastName,
    decimal TotalAmount,
    DateOnly PaymentDate,
    string? PaymentMethod,
    string? ReferenceNumber,
    string? ReceiptNumber,
    bool IsPartial,
    long? AccountingEntryId,
    string? EntryNumber,
    List<PaymentItem> AppliedItems,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);



public sealed record AppliedPaymentItem(
    long PaymentItemId,
    long MemberFeeId,
    decimal AmountApplied
);
