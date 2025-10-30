using NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployeeTypes;

namespace NexaSoft.Club.Domain.Specifications;

public class PayrollConceptEmployeeTypeSpecification : BaseSpecification<PayrollConceptEmployeeType, PayrollConceptEmployeeTypeResponse>
{
    public BaseSpecParams SpecParams { get; }

    public PayrollConceptEmployeeTypeSpecification(BaseSpecParams specParams) : base()
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
                case "employeetypeid":
                    if (long.TryParse(specParams.Search, out var searchNumberEmployeeTypeId))
                        AddCriteria(x => x.EmployeeTypeId == searchNumberEmployeeTypeId);
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

      AddSelect(x => new PayrollConceptEmployeeTypeResponse(
             x.Id,
             x.PayrollConceptId,
             x.PayrollConcept!.Code!,
             x.EmployeeTypeId,
             x.EmployeeType!.Code!,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
