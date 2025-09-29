using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Features.Payments;

namespace NexaSoft.Club.Application.Features.Payments.Queries.GetPayment;

public sealed record GetPaymentQuery(
    long Id
) : IQuery<PaymentResponse>;
