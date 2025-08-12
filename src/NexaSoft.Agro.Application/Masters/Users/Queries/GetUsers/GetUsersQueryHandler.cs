using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Masters.Users;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Application.Masters.Users.Queries.GetUsers;

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
