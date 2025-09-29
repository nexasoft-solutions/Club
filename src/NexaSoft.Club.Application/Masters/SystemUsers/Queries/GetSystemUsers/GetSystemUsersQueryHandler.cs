using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.SystemUsers;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.SystemUsers.Queries.GetSystemUsers;

public class GetSystemUsersQueryHandler(
    IGenericRepository<SystemUser> _repository
) : IQueryHandler<GetSystemUsersQuery, Pagination<SystemUserResponse>>
{
    public async Task<Result<Pagination<SystemUserResponse>>> Handle(GetSystemUsersQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new SystemUserSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<SystemUserResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<SystemUserResponse>(
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
            return Result.Failure<Pagination<SystemUserResponse>>(SystemUserErrores.ErrorConsulta);
        }
    }
}
