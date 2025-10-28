using NexaSoft.Club.Domain.Masters.FeeConfigurations;

namespace NexaSoft.Club.Domain.Specifications;

public class FeeConfigurationSpecification : BaseSpecification<FeeConfiguration, FeeConfigurationResponse>
{
    public BaseSpecParams SpecParams { get; }

    public FeeConfigurationSpecification(BaseSpecParams specParams) : base()
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

                    case "feename":
                        AddCriteria(x => x.FeeName != null && x.FeeName.ToLower().Contains(specParams.Search.ToLower()));
                        break;
                    case "periodicity":
                        if (long.TryParse(specParams.Search, out var searchNumber))
                            AddCriteria(x => x.PeriodicityId == searchNumber);
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
                case "fenameasc":
                    AddOrderBy(x => x.FeeName!);
                    break;
                case "fenamedesc":
                    AddOrderByDescending(x => x.FeeName!);
                    break;
                default:
                    AddOrderBy(x => x.FeeName!);
                    break;
            }
        }


    /*    
    */
        AddSelect(x => new FeeConfigurationResponse(
             x.Id,
             x.FeeName,
             x.PeriodicityId,
             x.Periodicity!.Name,
             x.DueDay,
             x.DefaultAmount,
             x.IsVariable,
             x.Priority,
             x.AppliesToFamily,
             x.IncomeAccountId,
             x.IncomeAccount!.AccountName!,
             x.IncomeAccount!.AccountCode!,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
