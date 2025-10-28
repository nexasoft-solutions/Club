using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.ContractTypes;

namespace NexaSoft.Club.Application.HumanResources.ContractTypes.Queries.GetContractTypes;

public sealed record GetContractTypesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<ContractTypeResponse>>;
