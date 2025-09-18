using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Users.Commands.RevokeRolesFromUser;

public sealed record RevokeRolesFromUserCommand(long UserId, List<long> RoleIds) 
    : ICommand<bool>;
