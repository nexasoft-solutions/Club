using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.ConceptCalculationTypes.Commands.DeleteConceptCalculationType;

public sealed record DeleteConceptCalculationTypeCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
