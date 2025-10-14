using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.SpaceTypes.Commands.CreateSpaceType;

public sealed record CreateSpaceTypeCommand(
    string? Name,
    string? Description,
    string CreatedBy
) : ICommand<long>;
