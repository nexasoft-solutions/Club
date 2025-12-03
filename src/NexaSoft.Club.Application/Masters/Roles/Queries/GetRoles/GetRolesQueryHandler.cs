using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Roles;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Application.Masters.Roles.Queries.GetRoles;

public class GetRolesQueryHandler(IGenericRepository<Role> _repository) : IQueryHandler<GetRolesQuery, Pagination<RoleResponse>>
{
    public async Task<Result<Pagination<RoleResponse>>> Handle(GetRolesQuery query, CancellationToken cancellationToken)
    {
        try
        {

            var spec = new RoleSpecification(query.SpecParams);
            var responses = await _repository.ListAsync(spec, cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<RoleResponse>(
                    query.SpecParams.PageIndex,
                    query.SpecParams.PageSize,
                    totalItems,
                    responses
              );

            return Result.Success(pagination);


        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<Pagination<RoleResponse>>(RoleErrores.ErrorConsulta);
        }
    }
}
