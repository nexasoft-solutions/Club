using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.SpacePhotos;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.SpacePhotos.Queries.GetSpacePhotos;

public class GetSpacePhotosQueryHandler(
    IGenericRepository<SpacePhoto> _repository
) : IQueryHandler<GetSpacePhotosQuery, Pagination<SpacePhotoResponse>>
{
    public async Task<Result<Pagination<SpacePhotoResponse>>> Handle(GetSpacePhotosQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new SpacePhotoSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<SpacePhotoResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<SpacePhotoResponse>(
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
            return Result.Failure<Pagination<SpacePhotoResponse>>(SpacePhotoErrores.ErrorConsulta);
        }
    }
}
