using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Users.Commands.RevokeRolesFromUser;

public sealed record RevokeRolesFromUserCommand(Guid UserId, List<Guid> RoleIds) 
    : ICommand<bool>;
