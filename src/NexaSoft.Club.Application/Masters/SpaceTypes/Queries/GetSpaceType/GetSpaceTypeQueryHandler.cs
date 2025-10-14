using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.SpaceTypes;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.SpaceTypes.Queries.GetSpaceType;

public class GetSpaceTypeQueryHandler(
    IGenericRepository<SpaceType> _repository
) : IQueryHandler<GetSpaceTypeQuery, SpaceTypeResponse>
{
    public async Task<Result<SpaceTypeResponse>> Handle(GetSpaceTypeQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new SpaceTypeSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<SpaceTypeResponse>(SpaceTypeErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<SpaceTypeResponse>(SpaceTypeErrores.ErrorConsulta);
        }
    }
}
