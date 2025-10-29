using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.PayrollConcepts;

namespace NexaSoft.Club.Application.HumanResources.PayrollConcepts.Queries.GetPayrollConcept;

public sealed record GetPayrollConceptQuery(
    long Id
) : IQuery<PayrollConceptResponse>;
