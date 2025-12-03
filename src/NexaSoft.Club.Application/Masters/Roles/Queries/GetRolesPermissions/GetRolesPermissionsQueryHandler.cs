using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.Roles;

namespace NexaSoft.Club.Application.Masters.Roles.Queries.GetRolesPermissions;

public class GetRolesPermissionsQueryHandler(
    IRolePermissionRepository _rolePermissionRepository
) : IQueryHandler<GetRolesPermissionsQuery, List<PermissionBasicResponse>>
{
    public async Task<Result<List<PermissionBasicResponse>>> Handle(GetRolesPermissionsQuery query, CancellationToken cancellationToken)
    {
        try
        {

            var responses = await _rolePermissionRepository.GetPermissionNamesForRoleAsync(query.RoleId, cancellationToken);

            return Result.Success(responses);

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<List<PermissionBasicResponse>>(RoleErrores.ErrorConsulta);
        }
    }
}
