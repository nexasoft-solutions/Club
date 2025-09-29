using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.SystemUsers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.SystemUsers.Queries.GetSystemUser;

public class GetSystemUserQueryHandler(
    IGenericRepository<SystemUser> _repository
) : IQueryHandler<GetSystemUserQuery, SystemUserResponse>
{
    public async Task<Result<SystemUserResponse>> Handle(GetSystemUserQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new SystemUserSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<SystemUserResponse>(SystemUserErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<SystemUserResponse>(SystemUserErrores.ErrorConsulta);
        }
    }
}
