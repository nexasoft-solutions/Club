using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.HumanResources.PayrollConcepts;
using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;

namespace NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployees;

public class PayrollConceptEmployee : Entity
{
    public long? PayrollConceptId { get; private set; }
    public PayrollConcept? PayrollConcept { get; private set; }
    public long? EmployeeId { get; private set; }
    public EmployeeInfo? EmployeeInfo { get; private set; }
    public int StatePayrollConceptEmployee { get; private set; }

    private PayrollConceptEmployee() { }

    private PayrollConceptEmployee(
        long? payrollConceptId, 
        long? employeeId, 
        int statePayrollConceptEmployee, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        PayrollConceptId = payrollConceptId;
        EmployeeId = employeeId;
        StatePayrollConceptEmployee = statePayrollConceptEmployee;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static PayrollConceptEmployee Create(
        long? payrollConceptId, 
        long? employeeId, 
        int statePayrollConceptEmployee, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new PayrollConceptEmployee(
            payrollConceptId,
            employeeId,
            statePayrollConceptEmployee,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        long? payrollConceptId, 
        long? employeeId, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        PayrollConceptId = payrollConceptId;
        EmployeeId = employeeId;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StatePayrollConceptEmployee = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
