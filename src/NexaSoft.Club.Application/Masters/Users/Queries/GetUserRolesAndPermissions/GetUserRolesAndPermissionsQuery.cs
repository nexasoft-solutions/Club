
using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Users.Queries.GetUserRolesAndPermissions;

public record GetUserRolesAndPermissionsQuery(long UserId) : IQuery<List<UserRolesPermissionsResponse>>;
