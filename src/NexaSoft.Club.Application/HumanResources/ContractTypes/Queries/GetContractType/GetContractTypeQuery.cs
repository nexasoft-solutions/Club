using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.ContractTypes;

namespace NexaSoft.Club.Application.HumanResources.ContractTypes.Queries.GetContractType;

public sealed record GetContractTypeQuery(
    long Id
) : IQuery<ContractTypeResponse>;
