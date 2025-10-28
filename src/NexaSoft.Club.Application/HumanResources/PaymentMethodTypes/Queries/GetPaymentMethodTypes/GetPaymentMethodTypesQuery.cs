using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.PaymentMethodTypes;

namespace NexaSoft.Club.Application.HumanResources.PaymentMethodTypes.Queries.GetPaymentMethodTypes;

public sealed record GetPaymentMethodTypesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<PaymentMethodTypeResponse>>;
