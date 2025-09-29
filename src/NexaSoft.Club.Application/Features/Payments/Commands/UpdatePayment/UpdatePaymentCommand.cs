using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.Payments.Commands.UpdatePayment;

public sealed record UpdatePaymentCommand(
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
) : ICommand<bool>;
