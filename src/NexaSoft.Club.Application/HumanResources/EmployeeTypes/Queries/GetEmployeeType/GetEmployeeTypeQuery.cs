using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.EmployeeTypes;

namespace NexaSoft.Club.Application.HumanResources.EmployeeTypes.Queries.GetEmployeeType;

public sealed record GetEmployeeTypeQuery(
    long Id
) : IQuery<EmployeeTypeResponse>;
