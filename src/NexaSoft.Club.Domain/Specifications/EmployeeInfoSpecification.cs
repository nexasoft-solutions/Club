using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;

namespace NexaSoft.Club.Domain.Specifications;

public class EmployeeInfoSpecification : BaseSpecification<EmployeeInfo, EmployeeInfoResponse>
{
    public BaseSpecParams SpecParams { get; }

    public EmployeeInfoSpecification(BaseSpecParams specParams) : base()
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
                case "employeecode":
                    AddCriteria(x => x.EmployeeCode != null && x.EmployeeCode.ToLower().Contains(specParams.Search.ToLower()));
                    break;
                case "userid":
                    if (long.TryParse(specParams.Search, out var searchNumberUserId))
                        AddCriteria(x => x.UserId == searchNumberUserId);
                    break;
                case "positionid":
                    if (long.TryParse(specParams.Search, out var searchNumberPositionId))
                        AddCriteria(x => x.PositionId == searchNumberPositionId);
                    break;
                case "employeetypeid":
                    if (long.TryParse(specParams.Search, out var searchNumberEmployeeTypeId))
                        AddCriteria(x => x.EmployeeTypeId == searchNumberEmployeeTypeId);
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
            case "employeecodeasc":
                AddOrderBy(x => x.EmployeeCode!);
                break;
            case "employeecodedesc":
                AddOrderByDescending(x => x.EmployeeCode!);
                break;
            default:
                AddOrderBy(x => x.EmployeeCode!);
                break;
        }
    }

      AddSelect(x => new EmployeeInfoResponse(
             x.Id,
             x.EmployeeCode,
             x.UserId,
             x.User!.UserName!,
             x.PositionId,
             x.Position!.Code!,
             x.EmployeeTypeId,
             x.EmployeeType!.Code!,
             x.DepartmentId,
             x.Department!.Code!,
             x.HireDate,
             x.BaseSalary,
             x.PaymentMethodId,
             x.PaymentMethodType!.Code!,
             x.BankId,
             x.Bank!.Code!,
             x.BankAccountTypeId,
             x.BankAccountType!.Code!,
             x.CurrencyId,
             x.Currency!.Code!,
             x.BankAccountNumber,
             x.CciNumber,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
