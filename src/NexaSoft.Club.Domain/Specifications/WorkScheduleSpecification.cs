using NexaSoft.Club.Domain.HumanResources.WorkSchedules;

namespace NexaSoft.Club.Domain.Specifications;

public class WorkScheduleSpecification : BaseSpecification<WorkSchedule, WorkScheduleResponse>
{
    public BaseSpecParams SpecParams { get; }

    public WorkScheduleSpecification(BaseSpecParams specParams) : base()
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

      AddSelect(x => new WorkScheduleResponse(
             x.Id,
             x.EmployeeId,
             x.EmployeeInfo!.EmployeeCode!,
             x.DayOfWeek,
             x.StartTime,
             x.EndTime,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
