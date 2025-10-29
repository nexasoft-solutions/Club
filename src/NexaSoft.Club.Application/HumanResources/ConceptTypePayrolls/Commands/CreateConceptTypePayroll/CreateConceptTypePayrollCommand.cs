using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.ConceptTypePayrolls.Commands.CreateConceptTypePayroll;

public sealed record CreateConceptTypePayrollCommand(
    string? Code,
    string? Name,
    string? Description,
    string CreatedBy
) : ICommand<long>;
