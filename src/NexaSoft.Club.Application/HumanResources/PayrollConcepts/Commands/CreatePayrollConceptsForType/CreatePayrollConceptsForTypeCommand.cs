using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollConcepts.Commands.CreatePayrollConceptsForType;

public sealed record CreatePayrollConceptsForTypeCommand(
    long PayrollTypeId,
    List<long> PayrollConceptIds,
    string? CreatedBy
) : ICommand<bool>;