using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.ConceptApplicationTypes.Commands.CreateConceptApplicationType;

public sealed record CreateConceptApplicationTypeCommand(
    string? Code,
    string? Name,
    string? Description,
    string CreatedBy
) : ICommand<long>;
