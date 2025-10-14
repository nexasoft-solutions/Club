using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.UserTypes;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.UserTypes.Queries.GetUserType;

public class GetUserTypeQueryHandler(
    IGenericRepository<UserType> _repository
) : IQueryHandler<GetUserTypeQuery, UserTypeResponse>
{
    public async Task<Result<UserTypeResponse>> Handle(GetUserTypeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new UserTypeSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<UserTypeResponse>(UserTypeErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<UserTypeResponse>(UserTypeErrores.ErrorConsulta);
        }
    }
}
