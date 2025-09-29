namespace NexaSoft.Club.Api.Controllers.Features.Payments.Request;

public sealed record UpdatePaymentRequest(
    long Id,
    long MemberId,
    long? FeeId,
    decimal Amount,
    DateOnly PaymentDate,
    string? PaymentMethod,
    string? ReferenceNumber,
    string? ReceiptNumber,
    bool IsPartial,
    long AccountingEntryId,
    string UpdatedBy
);
