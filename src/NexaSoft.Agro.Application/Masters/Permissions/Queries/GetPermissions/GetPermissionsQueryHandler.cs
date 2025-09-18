
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Permissions;

namespace NexaSoft.Agro.Application.Masters.Permissions.Queries.GetPermissions;

public class GetPermissionsQueryHandler(IGenericRepository<Permission> _repository) : IQueryHandler<GetPermissionsQuery, List<PermissionResponse>>
{
    public async Task<Result<List<PermissionResponse>>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        try
        {

            var response = await _repository.ListAsync(cancellationToken);

            var list = response.Select(p => new PermissionResponse(
                p.Id,
                p.Name,
                p.Description,
                p.ReferenciaControl,
                p.FechaCreacion,
                p.FechaModificacion,
                p.UsuarioCreacion,
                p.UsuarioModificacion
            )).ToList();


            return Result.Success(list);

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<List<PermissionResponse>>(PermissionErrores.ErrorConsulta);
        }
    }
}
