using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.Spaces;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.Spaces.Queries.GetSpace;

public class GetSpaceQueryHandler(
    IGenericRepository<Space> _repository
) : IQueryHandler<GetSpaceQuery, SpaceResponse>
{
    public async Task<Result<SpaceResponse>> Handle(GetSpaceQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new SpaceSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<SpaceResponse>(SpaceErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<SpaceResponse>(SpaceErrores.ErrorConsulta);
        }
    }
}
