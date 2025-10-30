using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.IncomeTaxScales;

namespace NexaSoft.Club.Application.HumanResources.IncomeTaxScales.Queries.GetIncomeTaxScales;

public sealed record GetIncomeTaxScalesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<IncomeTaxScaleResponse>>;
