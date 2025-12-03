using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Permissions;

namespace NexaSoft.Club.Application.Masters.Permissions.Queries.GetPermissionsByReference;

public class GetPermissionByReferenceQueryHandler(
    IGenericRepository<Permission> _repository
) : IQueryHandler<GetPermissionByReferenceQuery, List<PermissionReferenceResponse>>
{
    public async Task<Result<List<PermissionReferenceResponse>>> Handle(GetPermissionByReferenceQuery query, CancellationToken cancellationToken)
    {
        try
        {

            var items = await _repository.ListAsync(cancellationToken);
            var grouped = items
                .OrderBy(p => p.Source)  
                .GroupBy(p => p.Source)
                .Select(g => new PermissionReferenceResponse(
                    Name: g.Key!,
                    Items: g.Select(p => new PermissionItemResponse(
                        Id: p.Id,
                        Name: p.Description!
                    )).ToList()               
                )).ToList();

            return Result.Success(grouped);


        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<List<PermissionReferenceResponse>>(PermissionErrores.ErrorConsulta);
        }
    }
}
