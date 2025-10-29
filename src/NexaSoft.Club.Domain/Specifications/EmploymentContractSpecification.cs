using NexaSoft.Club.Domain.HumanResources.EmploymentContracts;

namespace NexaSoft.Club.Domain.Specifications;

public class EmploymentContractSpecification : BaseSpecification<EmploymentContract, EmploymentContractResponse>
{
    public BaseSpecParams SpecParams { get; }

    public EmploymentContractSpecification(BaseSpecParams specParams) : base()
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
                case "contracttypeid":
                    if (long.TryParse(specParams.Search, out var searchNumberContractTypeId))
                        AddCriteria(x => x.ContractTypeId == searchNumberContractTypeId);
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
            case "contracttypeidasc":
                AddOrderBy(x => x.ContractTypeId!);
                break;
            case "contracttypeiddesc":
                AddOrderByDescending(x => x.ContractTypeId!);
                break;
            default:
                AddOrderBy(x => x.ContractTypeId!);
                break;
        }
    }

      AddSelect(x => new EmploymentContractResponse(
             x.Id,
             x.EmployeeId,
             x.EmployeeInfo!.EmployeeCode!,
             x.ContractTypeId,
             x.ContractType!.Code!,
             x.StartDate,
             x.EndDate,
             x.Salary,
             x.WorkingHours,
             x.DocumentPath,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
