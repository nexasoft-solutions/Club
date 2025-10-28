using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.ConceptApplicationTypes.Commands.UpdateConceptApplicationType;

public sealed record UpdateConceptApplicationTypeCommand(
    long Id,
    string? Code,
    string? Name,
    string? Description,
    string UpdatedBy
) : ICommand<bool>;
