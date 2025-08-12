
using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Users.Queries.GetUserRolesAndPermissions;

public record GetUserRolesAndPermissionsQuery(Guid UserId) : IQuery<List<UserRolesPermissionsResponse>>;
