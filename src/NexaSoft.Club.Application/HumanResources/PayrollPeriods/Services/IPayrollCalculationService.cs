using NexaSoft.Club.Domain.HumanResources.AttendanceRecords;
using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;
using NexaSoft.Club.Domain.HumanResources.PayrollConcepts;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriods.Services;

public interface IPayrollCalculationService
{
   Task<decimal> CalculateConceptValue(PayrollConcept concept, EmployeeInfo employee,
        List<AttendanceRecord> attendance, int year, CancellationToken cancellationToken);
    
    Task<decimal> CalculateTotalIncome(EmployeeInfo employee, List<AttendanceRecord> attendance,
        int year, CancellationToken cancellationToken);
        
    Task<decimal> CalculateTotalDeductions(EmployeeInfo employee, List<AttendanceRecord> attendance,
        int year, CancellationToken cancellationToken);
    
    Task<Dictionary<string, object>> GetCalculationVariables(EmployeeInfo employee,
        List<AttendanceRecord> attendance, int year, CancellationToken cancellationToken);
}
