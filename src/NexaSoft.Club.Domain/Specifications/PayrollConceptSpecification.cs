using NexaSoft.Club.Domain.HumanResources.PayrollConcepts;

namespace NexaSoft.Club.Domain.Specifications;

public class PayrollConceptSpecification : BaseSpecification<PayrollConcept, PayrollConceptResponse>
{
    public BaseSpecParams SpecParams { get; }

    public PayrollConceptSpecification(BaseSpecParams specParams) : base()
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

      AddSelect(x => new PayrollConceptResponse(
             x.Id,
             x.Code,
             x.Name,
             x.ConceptTypePayrollId,
             x.ConceptTypePayroll!.Code!,
             x.PayrollFormulaId,
             x.PayrollFormula!.Code!,
             x.ConceptCalculationTypeId,
             x.ConceptCalculationType!.Code!,
             x.FixedValue,
             x.PorcentajeValue,
             x.ConceptApplicationTypesId,
             x.ConceptApplicationType!.Code!,
             x.AccountingChartId,
             x.AccountingChart!.AccountName!,
             x.PayrollTypeId,
             x.PayrollType!.Code!,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
