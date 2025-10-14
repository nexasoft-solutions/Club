using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Ubigeos.Commands.UpdateUbigeo;

public sealed record UpdateUbigeoCommand(
    long Id,
    string? Description,
    int Level,
    long? ParentId,
    string UserModification
) : ICommand<bool>;
