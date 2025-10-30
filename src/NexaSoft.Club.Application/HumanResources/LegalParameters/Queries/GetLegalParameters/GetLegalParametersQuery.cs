using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.LegalParameters;

namespace NexaSoft.Club.Application.HumanResources.LegalParameters.Queries.GetLegalParameters;

public sealed record GetLegalParametersQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<LegalParameterResponse>>;
