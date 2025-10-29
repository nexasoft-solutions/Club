using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.PayrollFormulas;

namespace NexaSoft.Club.Application.HumanResources.PayrollFormulas.Queries.GetPayrollFormulas;

public sealed record GetPayrollFormulasQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<PayrollFormulaResponse>>;
