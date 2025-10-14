using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Masters.PaymentTypes;

namespace NexaSoft.Club.Application.Masters.PaymentTypes.Queries.GetPaymentTypes;

public sealed record GetPaymentTypesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<PaymentTypeResponse>>;
