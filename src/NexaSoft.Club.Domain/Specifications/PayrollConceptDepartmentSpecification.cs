using NexaSoft.Club.Domain.HumanResources.PayrollConceptDepartments;

namespace NexaSoft.Club.Domain.Specifications;

public class PayrollConceptDepartmentSpecification : BaseSpecification<PayrollConceptDepartment, PayrollConceptDepartmentResponse>
{
    public BaseSpecParams SpecParams { get; }

    public PayrollConceptDepartmentSpecification(BaseSpecParams specParams) : base()
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
                case "payrollconceptid":
                    if (long.TryParse(specParams.Search, out var searchNumberPayrollConceptId))
                        AddCriteria(x => x.PayrollConceptId == searchNumberPayrollConceptId);
                    break;
                case "departmentid":
                    if (long.TryParse(specParams.Search, out var searchNumberDepartmentId))
                        AddCriteria(x => x.DepartmentId == searchNumberDepartmentId);
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
            case "payrollconceptidasc":
                AddOrderBy(x => x.PayrollConceptId!);
                break;
            case "payrollconceptiddesc":
                AddOrderByDescending(x => x.PayrollConceptId!);
                break;
            default:
                AddOrderBy(x => x.PayrollConceptId!);
                break;
        }
    }

      AddSelect(x => new PayrollConceptDepartmentResponse(
             x.Id,
             x.PayrollConceptId,
             x.PayrollConcept!.Code!,
             x.DepartmentId,
             x.Department!.Code!,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
