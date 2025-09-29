using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.Spaces;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.Spaces.Queries.GetSpaces;

public class GetSpacesQueryHandler(
    IGenericRepository<Space> _repository
) : IQueryHandler<GetSpacesQuery, Pagination<SpaceResponse>>
{
    public async Task<Result<Pagination<SpaceResponse>>> Handle(GetSpacesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new SpaceSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<SpaceResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<SpaceResponse>(
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
            return Result.Failure<Pagination<SpaceResponse>>(SpaceErrores.ErrorConsulta);
        }
    }
}
