using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.PayPeriodTypes;

namespace NexaSoft.Club.Application.HumanResources.PayPeriodTypes.Queries.GetPayPeriodType;

public sealed record GetPayPeriodTypeQuery(
    long Id
) : IQuery<PayPeriodTypeResponse>;
