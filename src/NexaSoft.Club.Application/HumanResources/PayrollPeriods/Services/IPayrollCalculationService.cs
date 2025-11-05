using NexaSoft.Club.Domain.HumanResources.AttendanceRecords;
using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;
using NexaSoft.Club.Domain.HumanResources.PayrollConcepts;
using NexaSoft.Club.Domain.HumanResources.PayrollFormulas;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriods.Services;

public interface IPayrollCalculationService
{
    Task<decimal> CalculateConceptValue(
        PayrollConcept concept,
        EmployeeInfoResponse employee,
        List<AttendanceRecord> attendance,
        int year,
        long? payrollTypeId,
        //PayrollPeriod? payrollPeriod,
        CancellationToken cancellationToken);

    Task<decimal> CalculateTotalIncome(EmployeeInfoResponse employee, List<AttendanceRecord> attendance,
        int year, CancellationToken cancellationToken);

    Task<decimal> CalculateTotalDeductions(EmployeeInfoResponse employee, List<AttendanceRecord> attendance,
        int year, CancellationToken cancellationToken);

    Task<Dictionary<string, object>> GetCalculationVariables(EmployeeInfoResponse employee,
        List<AttendanceRecord> attendance, int year, CancellationToken cancellationToken);

    // Nueva sobrecarga dinámica
    Task<Dictionary<string, object>> GetCalculationVariables(EmployeeInfoResponse employee,
        List<AttendanceRecord> attendance, int year, CancellationToken cancellationToken,
        PayrollConcept? concept, PayrollFormula? formula);
}
