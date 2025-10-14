using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.UserTypes;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.UserTypes.Queries.GetUserTypes;

public class GetUserTypesQueryHandler(
    IGenericRepository<UserType> _repository
) : IQueryHandler<GetUserTypesQuery, Pagination<UserTypeResponse>>
{
    public async Task<Result<Pagination<UserTypeResponse>>> Handle(GetUserTypesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new UserTypeSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<UserTypeResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<UserTypeResponse>(
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
            return Result.Failure<Pagination<UserTypeResponse>>(UserTypeErrores.ErrorConsulta);
        }
    }
}
