using NexaSoft.Club.Domain.HumanResources.CostCenters;

namespace NexaSoft.Club.Domain.Specifications;

public class CostCenterSpecification : BaseSpecification<CostCenter, CostCenterResponse>
{
    public BaseSpecParams SpecParams { get; }

    public CostCenterSpecification(BaseSpecParams specParams) : base()
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
                case "parentcostcenterid":
                    if (long.TryParse(specParams.Search, out var searchNumberParentCostCenterId))
                        AddCriteria(x => x.ParentCostCenterId == searchNumberParentCostCenterId);
                    break;
                case "costcentertypeid":
                    if (long.TryParse(specParams.Search, out var searchNumberCostCenterTypeId))
                        AddCriteria(x => x.CostCenterTypeId == searchNumberCostCenterTypeId);
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

      AddSelect(x => new CostCenterResponse(
             x.Id,
             x.Code,
             x.Name,
             x.ParentCostCenterId,
             x.ParentCostCenter!.Name!,
             x.CostCenterTypeId,
             x.CostCenterType!.Name!,
             x.Description,
             x.ResponsibleId,
             x.EmployeeInfo!.EmployeeCode!,
             x.Budget,
             x.StartDate,
             x.EndDate,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
