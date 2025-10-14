using NexaSoft.Club.Domain.Masters.SpacePhotos;

namespace NexaSoft.Club.Domain.Specifications;

public class SpacePhotoSpecification : BaseSpecification<SpacePhoto, SpacePhotoResponse>
{
    public BaseSpecParams SpecParams { get; }

    public SpacePhotoSpecification(BaseSpecParams specParams) : base()
    {

       SpecParams = specParams;

    if (specParams.Id.HasValue)
    {
       AddCriteria(x => x.Id == specParams.Id.Value);
    }
    else
    {
        if (!string.IsNullOrEmpty(specParams.Search) && !string.IsNullOrEmpty(specParams.SearchFields))
        {
            switch (specParams.SearchFields.ToLower())
            {
                default:
                    Criteria = x => true;
                    break;
            }
        }


        // Aplicar paginación
        if (!specParams.NoPaging)
        {
           ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }

        // Aplicar ordenamiento
        switch (specParams.Sort)
        {
            case "idasc":
                AddOrderBy(x => x.Id!);
                break;
            case "iddesc":
                AddOrderByDescending(x => x.Id!);
                break;
            default:
                AddOrderBy(x => x.Id!);
                break;
        }
    }

      AddSelect(x => new SpacePhotoResponse(
             x.Id,
             x.SpaceId,
             x.Space!.SpaceName!,
             x.PhotoUrl,
             x.Order,
             x.Description,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
