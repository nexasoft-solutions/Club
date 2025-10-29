using NexaSoft.Club.Domain.HumanResources.Expenses;

namespace NexaSoft.Club.Domain.Specifications;

public class ExpenseSpecification : BaseSpecification<Expense, ExpenseResponse>
{
    public BaseSpecParams SpecParams { get; }

    public ExpenseSpecification(BaseSpecParams specParams) : base()
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
                case "costcenterid":
                    if (long.TryParse(specParams.Search, out var searchNumberCostCenterId))
                        AddCriteria(x => x.CostCenterId == searchNumberCostCenterId);
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
            case "costcenteridasc":
                AddOrderBy(x => x.CostCenterId!);
                break;
            case "costcenteriddesc":
                AddOrderByDescending(x => x.CostCenterId!);
                break;
            default:
                AddOrderBy(x => x.CostCenterId!);
                break;
        }
    }

      AddSelect(x => new ExpenseResponse(
             x.Id,
             x.CostCenterId,
             x.CostCenter!.Code!,
             x.Description,
             x.ExpenseDate,
             x.Amount,
             x.DocumentNumber,
             x.DocumentPath,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
