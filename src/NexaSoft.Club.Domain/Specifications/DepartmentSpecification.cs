using NexaSoft.Club.Domain.HumanResources.Departments;

namespace NexaSoft.Club.Domain.Specifications;

public class DepartmentSpecification : BaseSpecification<Department, DepartmentResponse>
{
    public BaseSpecParams SpecParams { get; }

    public DepartmentSpecification(BaseSpecParams specParams) : base()
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
                case "parentdepartmentid":
                    if (long.TryParse(specParams.Search, out var searchNumberParentDepartmentId))
                        AddCriteria(x => x.ParentDepartmentId == searchNumberParentDepartmentId);
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

      AddSelect(x => new DepartmentResponse(
             x.Id,
             x.Code,
             x.Name,
             x.ParentDepartmentId,
             x.ParentDepartment!.Code!,
             x.Description,
             x.ManagerId,
             x.EmployeeInfo!.EmployeeCode!,
             x.CostCenterId,
             x.CostCenter!.Code!,
             x.Location,
             x.PhoneExtension,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
