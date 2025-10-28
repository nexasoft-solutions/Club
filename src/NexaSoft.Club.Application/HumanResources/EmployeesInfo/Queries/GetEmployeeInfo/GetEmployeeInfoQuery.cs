using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;

namespace NexaSoft.Club.Application.HumanResources.EmployeesInfo.Queries.GetEmployeeInfo;

public sealed record GetEmployeeInfoQuery(
    long Id
) : IQuery<EmployeeInfoResponse>;
