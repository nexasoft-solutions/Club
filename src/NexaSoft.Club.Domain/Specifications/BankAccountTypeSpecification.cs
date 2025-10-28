using NexaSoft.Club.Domain.HumanResources.BankAccountTypes;

namespace NexaSoft.Club.Domain.Specifications;

public class BankAccountTypeSpecification : BaseSpecification<BankAccountType, BankAccountTypeResponse>
{
    public BaseSpecParams SpecParams { get; }

    public BankAccountTypeSpecification(BaseSpecParams specParams) : base()
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
                case "code":
                    AddCriteria(x => x.Code != null && x.Code.ToLower().Contains(specParams.Search.ToLower()));
                    break;
                case "name":
                    AddCriteria(x => x.Name != null && x.Name.ToLower().Contains(specParams.Search.ToLower()));
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
            case "codeasc":
                AddOrderBy(x => x.Code!);
                break;
            case "codedesc":
                AddOrderByDescending(x => x.Code!);
                break;
            default:
                AddOrderBy(x => x.Code!);
                break;
        }
    }

      AddSelect(x => new BankAccountTypeResponse(
             x.Id,
             x.Code,
             x.Name,
             x.Description,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
