using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.Users;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.Users.Queries.GetUsers;

public class GetUsersQueryHandler(
    IGenericRepository<User> _repository
) : IQueryHandler<GetUsersQuery, Pagination<UserResponse>>
{
    public async Task<Result<Pagination<UserResponse>>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new UserSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<UserResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<UserResponse>(
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
            return Result.Failure<Pagination<UserResponse>>(UserErrores.ErrorConsulta);
        }
    }
}
