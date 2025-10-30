using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployeeTypes;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptEmployeeTypes.Queries.GetPayrollConceptEmployeeType;

public sealed record GetPayrollConceptEmployeeTypeQuery(
    long Id
) : IQuery<PayrollConceptEmployeeTypeResponse>;
