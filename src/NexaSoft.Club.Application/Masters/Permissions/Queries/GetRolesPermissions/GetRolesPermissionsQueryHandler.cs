using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Masters.Roles;
using NexaSoft.Club.Domain.Masters.Permissions;
using NexaSoft.Club.Domain.Masters.Roles;

namespace NexaSoft.Club.Application.Masters.Permissions.Queries.GetRolesPermissions;

public class GetRolesPermissionsQueryHandler(IRolePermissionRepository _repository) : IQueryHandler<GetRolesPermissionsQuery, List<RolesPermissionsResponse>>
{
    public async Task<Result<List<RolesPermissionsResponse>>> Handle(GetRolesPermissionsQuery request, CancellationToken cancellationToken)
    {
        try
        {

            var response = await _repository.GetRolesPermissionsAsync(cancellationToken);

            var list = response.Select(p => new RolesPermissionsResponse(
                p.RoleId,
                p.NameRol,
                p.PermissionId,
                p.NamePermission,
                p.Reference,
                p.Source,
                p.Action
            )).ToList();


            return Result.Success(list);

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            Console.WriteLine($"Error=> {errores}");
            return Result.Failure<List<RolesPermissionsResponse>>(PermissionErrores.ErrorConsulta);
        }
    }
}
