using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.Users;

namespace NexaSoft.Club.Application.Masters.Users.Commands.AssignRolesToUser;

public sealed record AssignRolesToUserCommand(long UserId, List<RoleDefultResponse> RoleDefs) 
    : ICommand<bool>;
