using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployees;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptEmployees.Queries.GetPayrollConceptEmployee;

public sealed record GetPayrollConceptEmployeeQuery(
    long Id
) : IQuery<PayrollConceptEmployeeResponse>;
