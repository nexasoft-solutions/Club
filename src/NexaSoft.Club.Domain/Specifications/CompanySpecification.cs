using NexaSoft.Club.Domain.HumanResources.Companies;

namespace NexaSoft.Club.Domain.Specifications;

public class CompanySpecification : BaseSpecification<Company, CompanyResponse>
{
    public BaseSpecParams SpecParams { get; }

    public CompanySpecification(BaseSpecParams specParams) : base()
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
                case "ruc":
                    AddCriteria(x => x.Ruc != null && x.Ruc.ToLower().Contains(specParams.Search.ToLower()));
                    break;
                case "businessname":
                    AddCriteria(x => x.BusinessName != null && x.BusinessName.ToLower().Contains(specParams.Search.ToLower()));
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
            case "rucasc":
                AddOrderBy(x => x.Ruc!);
                break;
            case "rucdesc":
                AddOrderByDescending(x => x.Ruc!);
                break;
            default:
                AddOrderBy(x => x.Ruc!);
                break;
        }
    }

      AddSelect(x => new CompanyResponse(
             x.Id,
             x.Ruc,
             x.BusinessName,
             x.TradeName,
             x.Address,
             x.Phone,
             x.Website,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
