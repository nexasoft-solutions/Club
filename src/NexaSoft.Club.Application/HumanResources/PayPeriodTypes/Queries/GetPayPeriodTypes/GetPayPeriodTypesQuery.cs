using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.PayPeriodTypes;

namespace NexaSoft.Club.Application.HumanResources.PayPeriodTypes.Queries.GetPayPeriodTypes;

public sealed record GetPayPeriodTypesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<PayPeriodTypeResponse>>;
