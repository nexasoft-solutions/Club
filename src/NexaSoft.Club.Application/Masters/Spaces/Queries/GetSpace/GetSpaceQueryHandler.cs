using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.Spaces;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Application.Storages;

namespace NexaSoft.Club.Application.Masters.Spaces.Queries.GetSpace;

public class GetSpaceQueryHandler(
    IGenericRepository<Space> _repository,
    IFileStorageService _fileStorageService
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

            // Aquí recorremos las fotos para generar las URLs pre-firmadas
            if (entity.SpacePhotos != null && entity.SpacePhotos.Any())
            {
                foreach (var photo in entity.SpacePhotos)
                {
                    photo.PhotoUrl = await _fileStorageService.GetPresignedUrlAsync(photo.PhotoUrl!, expirationInMinutes: 60);
                }
            }

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<SpaceResponse>(SpaceErrores.ErrorConsulta);
        }
    }
}
