using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.IncomeTaxScales;

namespace NexaSoft.Club.Application.HumanResources.IncomeTaxScales.Queries.GetIncomeTaxScale;

public sealed record GetIncomeTaxScaleQuery(
    long Id
) : IQuery<IncomeTaxScaleResponse>;
