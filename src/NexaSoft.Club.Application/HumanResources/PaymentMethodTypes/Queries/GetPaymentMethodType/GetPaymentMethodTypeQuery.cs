using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.PaymentMethodTypes;

namespace NexaSoft.Club.Application.HumanResources.PaymentMethodTypes.Queries.GetPaymentMethodType;

public sealed record GetPaymentMethodTypeQuery(
    long Id
) : IQuery<PaymentMethodTypeResponse>;
