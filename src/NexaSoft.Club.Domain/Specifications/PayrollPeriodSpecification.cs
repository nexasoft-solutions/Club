using NexaSoft.Club.Domain.HumanResources.PayrollPeriods;

namespace NexaSoft.Club.Domain.Specifications;

public class PayrollPeriodSpecification : BaseSpecification<PayrollPeriod, PayrollPeriodResponse>
{
    public BaseSpecParams SpecParams { get; }

    public PayrollPeriodSpecification(BaseSpecParams specParams) : base()
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
                case "periodname":
                    AddCriteria(x => x.PeriodName != null && x.PeriodName.ToLower().Contains(specParams.Search.ToLower()));
                    break;
                case "statusid":
                    if (long.TryParse(specParams.Search, out var searchNumberStatusId))
                        AddCriteria(x => x.StatusId == searchNumberStatusId);
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
            case "periodnameasc":
                AddOrderBy(x => x.PeriodName!);
                break;
            case "periodnamedesc":
                AddOrderByDescending(x => x.PeriodName!);
                break;
            default:
                AddOrderBy(x => x.PeriodName!);
                break;
        }
    }

      AddSelect(x => new PayrollPeriodResponse(
             x.Id,
             x.PeriodName,
             x.StartDate,
             x.EndDate,
             x.TotalAmount,
             x.TotalEmployees,
             x.StatusId,
             x.Status!.Code!,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
