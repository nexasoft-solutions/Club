using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.ConceptApplicationTypes.Commands.DeleteConceptApplicationType;

public sealed record DeleteConceptApplicationTypeCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
