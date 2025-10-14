namespace NexaSoft.Club.Domain.Features.Payments;

public sealed record PaymentResponse(
    long Id,
    long MemberId,
    string? MemberFullName,
    decimal TotalAmount,
    DateOnly PaymentDate,
    long PaymentMethodId,
    string? ReferenceNumber,
    long DocumentTypeId,
    string? ReceiptNumber,
    bool IsPartial,
    long? AccountingEntryId,
    string? EntryNumber,
    long StatusId,
    string? PaymentMethodDescription,
    string? DocumentTypeDescription,
    string? StatusDescription,
    List<PaymentDetailResponse> AppliedItems,
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



public sealed record PaymentDetailResponse(
    long Id,
    long PaymentId,
    long MemberFeeId,
    decimal Amount,
    string? Concept,
    string? Period
);
