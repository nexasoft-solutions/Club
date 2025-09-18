using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Masters.Users;

namespace NexaSoft.Agro.Application.Masters.Users.Commands.AssignRolesToUser;

public sealed record AssignRolesToUserCommand(long UserId, List<RoleDefultResponse> RoleDefs) 
    : ICommand<bool>;
