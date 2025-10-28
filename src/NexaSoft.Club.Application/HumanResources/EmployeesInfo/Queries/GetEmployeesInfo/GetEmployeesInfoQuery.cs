using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;

namespace NexaSoft.Club.Application.HumanResources.EmployeesInfo.Queries.GetEmployeesInfo;

public sealed record GetEmployeesInfoQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<EmployeeInfoResponse>>;
