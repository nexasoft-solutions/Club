using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.PayrollFormulas;

namespace NexaSoft.Club.Application.HumanResources.PayrollFormulas.Queries.GetPayrollFormula;

public sealed record GetPayrollFormulaQuery(
    long Id
) : IQuery<PayrollFormulaResponse>;
