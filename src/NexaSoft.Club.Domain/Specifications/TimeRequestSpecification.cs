using NexaSoft.Club.Domain.HumanResources.TimeRequests;

namespace NexaSoft.Club.Domain.Specifications;

public class TimeRequestSpecification : BaseSpecification<TimeRequest, TimeRequestResponse>
{
    public BaseSpecParams SpecParams { get; }

    public TimeRequestSpecification(BaseSpecParams specParams) : base()
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
                case "employeeid":
                    if (long.TryParse(specParams.Search, out var searchNumberEmployeeId))
                        AddCriteria(x => x.EmployeeId == searchNumberEmployeeId);
                    break;
                case "timerequesttypeid":
                    if (long.TryParse(specParams.Search, out var searchNumberTimeRequestTypeId))
                        AddCriteria(x => x.TimeRequestTypeId == searchNumberTimeRequestTypeId);
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
            case "employeeidasc":
                AddOrderBy(x => x.EmployeeId!);
                break;
            case "employeeiddesc":
                AddOrderByDescending(x => x.EmployeeId!);
                break;
            default:
                AddOrderBy(x => x.EmployeeId!);
                break;
        }
    }

      AddSelect(x => new TimeRequestResponse(
             x.Id,
             x.EmployeeId,
             x.EmployeeInfo!.EmployeeCode!,
             x.TimeRequestTypeId,
             x.TimeRequestType!.Code!,
             x.StartDate,
             x.EndDate,
             x.TotalDays,
             x.Reason,
             x.StatusId,
             x.Status!.Name!,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
