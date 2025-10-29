using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.ConceptTypePayrolls.Commands.UpdateConceptTypePayroll;

public sealed record UpdateConceptTypePayrollCommand(
    long Id,
    string? Code,
    string? Name,
    string? Description,
    string UpdatedBy
) : ICommand<bool>;
