using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.UserTypes.Commands.CreateUserType;

public sealed record CreateUserTypeCommand(
    string? Name,
    string? Description,
    bool IsAdministrative,
    string CreatedBy
) : ICommand<long>;
