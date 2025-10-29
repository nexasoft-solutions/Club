using NexaSoft.Club.Domain.HumanResources.SpecialRates;

namespace NexaSoft.Club.Domain.Specifications;

public class SpecialRateSpecification : BaseSpecification<SpecialRate, SpecialRateResponse>
{
    public BaseSpecParams SpecParams { get; }

    public SpecialRateSpecification(BaseSpecParams specParams) : base()
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
                case "ratetypeid":
                    if (long.TryParse(specParams.Search, out var searchNumberRateTypeId))
                        AddCriteria(x => x.RateTypeId == searchNumberRateTypeId);
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
            case "ratetypeidasc":
                AddOrderBy(x => x.RateTypeId!);
                break;
            case "ratetypeiddesc":
                AddOrderByDescending(x => x.RateTypeId!);
                break;
            default:
                AddOrderBy(x => x.RateTypeId!);
                break;
        }
    }

      AddSelect(x => new SpecialRateResponse(
             x.Id,
             x.RateTypeId,
             x.RateType!.Code!,
             x.Name,
             x.Multiplier,
             x.StartTime,
             x.EndTime,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
