using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.SpaceTypes.Commands.UpdateSpaceType;

public sealed record UpdateSpaceTypeCommand(
    long Id,
    string? Name,
    string? Description,
    string UpdatedBy
) : ICommand<bool>;
