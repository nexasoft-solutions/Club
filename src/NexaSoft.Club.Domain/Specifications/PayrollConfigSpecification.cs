using NexaSoft.Club.Domain.HumanResources.PayrollConfigs;

namespace NexaSoft.Club.Domain.Specifications;

public class PayrollConfigSpecification : BaseSpecification<PayrollConfig, PayrollConfigResponse>
{
    public BaseSpecParams SpecParams { get; }

    public PayrollConfigSpecification(BaseSpecParams specParams) : base()
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
                case "companyid":
                    if (long.TryParse(specParams.Search, out var searchNumberCompanyId))
                        AddCriteria(x => x.CompanyId == searchNumberCompanyId);
                    break;
                case "payperiodtypeid":
                    if (long.TryParse(specParams.Search, out var searchNumberPayPeriodTypeId))
                        AddCriteria(x => x.PayPeriodTypeId == searchNumberPayPeriodTypeId);
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
            case "companyidasc":
                AddOrderBy(x => x.CompanyId!);
                break;
            case "companyiddesc":
                AddOrderByDescending(x => x.CompanyId!);
                break;
            default:
                AddOrderBy(x => x.CompanyId!);
                break;
        }
    }

      AddSelect(x => new PayrollConfigResponse(
             x.Id,
             x.CompanyId,
             x.Company!.BusinessName!,
             x.PayPeriodTypeId,
             x.PayPeriodType!.Code!,
             x.RegularHoursPerDay,
             x.WorkDaysPerWeek,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
