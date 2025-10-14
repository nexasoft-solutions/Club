using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.UserTypes.Commands.UpdateUserType;

public sealed record UpdateUserTypeCommand(
    long Id,
    string? Name,
    string? Description,
    bool IsAdministrative,
    string UpdatedBy
) : ICommand<bool>;
