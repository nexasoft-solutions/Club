using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.ConceptTypePayrolls;

namespace NexaSoft.Club.Application.HumanResources.ConceptTypePayrolls.Queries.GetConceptTypePayroll;

public sealed record GetConceptTypePayrollQuery(
    long Id
) : IQuery<ConceptTypePayrollResponse>;
