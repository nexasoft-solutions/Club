using NexaSoft.Club.Domain.Masters.MemberTypes;

namespace NexaSoft.Club.Domain.Specifications;

public class MemberTypeSpecification : BaseSpecification<MemberType, MemberTypeResponse>
{
    public BaseSpecParams SpecParams { get; }

    public MemberTypeSpecification(BaseSpecParams specParams) : base()
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
                case "typename":
                    AddCriteria(x => x.TypeName != null && x.TypeName.ToLower().Contains(specParams.Search.ToLower()));
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
            case "typenameasc":
                AddOrderBy(x => x.TypeName!);
                break;
            case "typenamedesc":
                AddOrderByDescending(x => x.TypeName!);
                break;
            default:
                AddOrderBy(x => x.TypeName!);
                break;
        }
    }

      AddSelect(x => new MemberTypeResponse(
             x.Id,
             x.TypeName,
             x.Description,
             //x.MonthlyFee,
             //x.EntryFee,
             x.HasFamilyDiscount,
             x.FamilyDiscountRate,
             x.MaxFamilyMembers,
             //x.IncomeAccountId,
             //x.IncomeAccount!.AccountName!,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
