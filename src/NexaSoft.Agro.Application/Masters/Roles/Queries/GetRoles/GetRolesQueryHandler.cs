using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Roles;

namespace NexaSoft.Agro.Application.Masters.Roles.Queries.GetRoles;

public class GetRolesQueryHandler(IGenericRepository<Role> _repository) : IQueryHandler<GetRolesQuery, List<RoleResponse>>
{
    public async Task<Result<List<RoleResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _repository.ListAsync(cancellationToken);

            var roles = result.Select(r => new RoleResponse
            (
                r.Id,
                r.Name,
                r.Description
            )).ToList();

            return Result.Success(roles);

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<List<RoleResponse>>(RoleErrores.ErrorConsulta);
        }
    }
}
