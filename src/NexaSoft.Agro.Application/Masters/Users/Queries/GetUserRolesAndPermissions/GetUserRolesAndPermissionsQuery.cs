
using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Users.Queries.GetUserRolesAndPermissions;

public record GetUserRolesAndPermissionsQuery(long UserId) : IQuery<List<UserRolesPermissionsResponse>>;
