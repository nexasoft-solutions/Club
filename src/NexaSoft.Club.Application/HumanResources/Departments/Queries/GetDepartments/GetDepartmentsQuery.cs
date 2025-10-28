using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.Departments;

namespace NexaSoft.Club.Application.HumanResources.Departments.Queries.GetDepartments;

public sealed record GetDepartmentsQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<DepartmentResponse>>;
