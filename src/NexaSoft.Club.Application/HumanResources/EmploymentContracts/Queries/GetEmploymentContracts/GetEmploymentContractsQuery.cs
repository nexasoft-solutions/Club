using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.EmploymentContracts;

namespace NexaSoft.Club.Application.HumanResources.EmploymentContracts.Queries.GetEmploymentContracts;

public sealed record GetEmploymentContractsQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<EmploymentContractResponse>>;
