using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Masters.Users;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Application.Masters.Users.Queries.GetUser;

public class GetUserQueryHandler(
    IGenericRepository<User> _repository
) : IQueryHandler<GetUserQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new UserSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<UserResponse>(UserErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<UserResponse>(UserErrores.ErrorConsulta);
        }
    }
}
