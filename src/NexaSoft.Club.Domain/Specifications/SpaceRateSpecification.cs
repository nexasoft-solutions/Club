using NexaSoft.Club.Domain.Masters.SpaceRates;

namespace NexaSoft.Club.Domain.Specifications;

public class SpaceRateSpecification : BaseSpecification<SpaceRate, SpaceRateResponse>
{
    public BaseSpecParams SpecParams { get; }

    public SpaceRateSpecification(BaseSpecParams specParams) : base()
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
                case "spaceid":
                    if (long.TryParse(specParams.Search, out var searchNumber))
                        AddCriteria(x => x.SpaceId == searchNumber);
                    break;
                case "membertypeid":
                    if (long.TryParse(specParams.Search, out var searchNumberType))
                        AddCriteria(x => x.MemberTypeId == searchNumberType);
                    break;
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
            case "spaceidasc":
                AddOrderBy(x => x.SpaceId!);
                break;
            case "spaceiddesc":
                AddOrderByDescending(x => x.SpaceId!);
                break;
            default:
                AddOrderBy(x => x.SpaceId!);
                break;
        }
    }

      AddSelect(x => new SpaceRateResponse(
             x.Id,
             x.SpaceId,
             x.Space!.SpaceName!,
             x.MemberTypeId,
             x.MemberType!.TypeName!,
             x.Rate,
             x.IsActive,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
