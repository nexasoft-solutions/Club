using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.ConceptTypePayrolls.Commands.DeleteConceptTypePayroll;

public sealed record DeleteConceptTypePayrollCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
