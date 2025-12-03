
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Permissions;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Application.Masters.Permissions.Queries.GetPermissions;

public class GetPermissionsQueryHandler(IGenericRepository<Permission> _repository) : IQueryHandler<GetPermissionsQuery, Pagination<PermissionResponse>>
{
    public async Task<Result<Pagination<PermissionResponse>>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        try
        {

            var spec = new PermissionSpecification(request.SpecParams);
            var responses = await _repository.ListAsync(spec, cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<PermissionResponse>(
                    request.SpecParams.PageIndex,
                    request.SpecParams.PageSize,
                    totalItems,
                    responses
              );

            return Result.Success(pagination);

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<Pagination<PermissionResponse>>(PermissionErrores.ErrorConsulta);
        }
    }
}
