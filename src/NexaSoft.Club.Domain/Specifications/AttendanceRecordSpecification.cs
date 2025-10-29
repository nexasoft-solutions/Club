using NexaSoft.Club.Domain.HumanResources.AttendanceRecords;

namespace NexaSoft.Club.Domain.Specifications;

public class AttendanceRecordSpecification : BaseSpecification<AttendanceRecord, AttendanceRecordResponse>
{
    public BaseSpecParams SpecParams { get; }

    public AttendanceRecordSpecification(BaseSpecParams specParams) : base()
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
                case "costcenterid":
                    if (long.TryParse(specParams.Search, out var searchNumberCostCenterId))
                        AddCriteria(x => x.CostCenterId == searchNumberCostCenterId);
                    break;
                case "attendancestatustypeid":
                    if (long.TryParse(specParams.Search, out var searchNumberAttendanceStatusTypeId))
                        AddCriteria(x => x.AttendanceStatusTypeId == searchNumberAttendanceStatusTypeId);
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

      AddSelect(x => new AttendanceRecordResponse(
             x.Id,
             x.EmployeeId,
             x.EmployeeInfo!.EmployeeCode!,
             x.CostCenterId,
             x.CostCenter!.Code!,
             x.RecordDate,
             x.CheckInTime,
             x.CheckOutTime,
             x.TotalHours,
             x.RegularHours,
             x.OvertimeHours,
             x.SundayHours,
             x.HolidayHours,
             x.NightHours,
             x.LateMinutes,
             x.EarlyDepartureMinutes,
             x.AttendanceStatusTypeId,
             x.AttendanceStatusType!.Code!,
             x.CreatedAt,
             x.UpdatedAt,
             x.CreatedBy,
             x.UpdatedBy
       ));
   }
}
