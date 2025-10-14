using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.Payments.Queries.GetReceiptByPayment;

public sealed record GetReceiptByPaymentQuery
(
    long PaymentId
):IQuery<byte[]>;
