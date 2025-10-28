using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.Departments;

namespace NexaSoft.Club.Application.HumanResources.Departments.Queries.GetDepartment;

public sealed record GetDepartmentQuery(
    long Id
) : IQuery<DepartmentResponse>;
