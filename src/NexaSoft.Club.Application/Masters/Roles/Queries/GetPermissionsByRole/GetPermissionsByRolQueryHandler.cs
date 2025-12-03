using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.Roles;

namespace NexaSoft.Club.Application.Masters.Roles.Queries.GetPermissionsByRole;

public class GetPermissionsByRolQueryHandler(
    IRolePermissionRepository _rolePermissionRepository
) : IQueryHandler<GetPermissionsByRolQuery, List<string>>
{
    public async Task<Result<List<string>>> Handle(GetPermissionsByRolQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var responses = await _rolePermissionRepository.GetPermissionNamesForRoleAsync(query.RoleId, cancellationToken);
            // Filtrar nulos para cumplir con List<string>
            var list = responses.Select(p => p.Name).Where(n => n != null).Select(n => n!).ToList();
            return Result.Success(list);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<List<string>>(RoleErrores.ErrorConsulta);
        }
    }
}
