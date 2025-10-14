using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.PaymentTypes;

namespace NexaSoft.Club.Application.Masters.PaymentTypes.Queries.GetPaymentType;

public sealed record GetPaymentTypeQuery(
    long Id
) : IQuery<PaymentTypeResponse>;
