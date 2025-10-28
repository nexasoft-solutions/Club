using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.EmployeeTypes;

namespace NexaSoft.Club.Application.HumanResources.EmployeeTypes.Queries.GetEmployeeTypes;

public sealed record GetEmployeeTypesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<EmployeeTypeResponse>>;
