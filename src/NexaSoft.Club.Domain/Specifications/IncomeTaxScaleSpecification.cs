using NexaSoft.Club.Domain.HumanResources.IncomeTaxScales;

namespace NexaSoft.Club.Domain.Specifications;

public class IncomeTaxScaleSpecification : BaseSpecification<IncomeTaxScale, IncomeTaxScaleResponse>
{
    public BaseSpecParams SpecParams { get; }

    public IncomeTaxScaleSpecification(BaseSpecParams specParams) : base()
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
                case "scaleyear":
                    if (long.TryParse(specParams.Search, out var searchNumberScaleYear))
                        AddCriteria(x => x.ScaleYear == searchNumberScaleYear);
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
            case "scaleyearasc":
                AddOrderBy(x => x.ScaleYear!);
                break;
            case "scaleyeardesc":
                AddOrderByDescending(x => x.ScaleYear!);
                break;
            default:
                AddOrderBy(x => x.ScaleYear!);
                break;
        }
    }

      AddSelect(x => new IncomeTaxScaleResponse(
             x.Id,
             x.ScaleYear,
             x.MinIncome,
             x.MaxIncome,
             x.FixedAmount,
             x.Rate,
             x.ExcessOver,
             x.Description,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
