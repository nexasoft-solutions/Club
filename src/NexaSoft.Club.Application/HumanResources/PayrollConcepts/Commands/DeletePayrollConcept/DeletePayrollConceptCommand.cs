using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollConcepts.Commands.DeletePayrollConcept;

public sealed record DeletePayrollConceptCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
