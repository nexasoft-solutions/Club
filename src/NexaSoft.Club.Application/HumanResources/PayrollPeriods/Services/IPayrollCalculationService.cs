using NexaSoft.Club.Domain.HumanResources.AttendanceRecords;
using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;
using NexaSoft.Club.Domain.HumanResources.PayrollConcepts;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriods.Services;

public interface IPayrollCalculationService
{
    Task<decimal> CalculateConceptValue(PayrollConcept concept, EmployeeInfo employee, 
        List<AttendanceRecord> attendance, CancellationToken cancellationToken);
        
    Task<Dictionary<string, object>> GetCalculationVariables(EmployeeInfo employee, 
        List<AttendanceRecord> attendance, CancellationToken cancellationToken);
        
    Task<bool> ConceptAppliesToEmployee(PayrollConcept concept, EmployeeInfo employee, 
        Dictionary<string, object> variables, CancellationToken cancellationToken);
    
    Task<decimal> CalculateTotalIncome(EmployeeInfo employee, List<AttendanceRecord> attendance,
        CancellationToken cancellationToken);
    
    Task<decimal> CalculateTotalDeductions(EmployeeInfo employee, List<AttendanceRecord> attendance,
        CancellationToken cancellationToken);
}
