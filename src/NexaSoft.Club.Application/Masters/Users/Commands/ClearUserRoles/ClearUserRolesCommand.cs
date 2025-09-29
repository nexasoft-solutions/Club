
using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Users.Commands.ClearUserRoles;

public sealed record ClearUserRolesCommand(long UserId) : ICommand<bool>;
