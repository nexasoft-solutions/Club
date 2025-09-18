
using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Users.Commands.ClearUserRoles;

public sealed record ClearUserRolesCommand(long UserId) : ICommand<bool>;
