using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.EmploymentContracts;

namespace NexaSoft.Club.Application.HumanResources.EmploymentContracts.Queries.GetEmploymentContract;

public sealed record GetEmploymentContractQuery(
    long Id
) : IQuery<EmploymentContractResponse>;
