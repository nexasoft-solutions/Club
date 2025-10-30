using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.HumanResources.PayrollConcepts;
using NexaSoft.Club.Domain.HumanResources.Departments;

namespace NexaSoft.Club.Domain.HumanResources.PayrollConceptDepartments;

public class PayrollConceptDepartment : Entity
{
    public long? PayrollConceptId { get; private set; }
    public PayrollConcept? PayrollConcept { get; private set; }
    public long? DepartmentId { get; private set; }
    public Department? Department { get; private set; }
    public int StatePayrollConceptDepartment { get; private set; }

    private PayrollConceptDepartment() { }

    private PayrollConceptDepartment(
        long? payrollConceptId, 
        long? departmentId, 
        int statePayrollConceptDepartment, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        PayrollConceptId = payrollConceptId;
        DepartmentId = departmentId;
        StatePayrollConceptDepartment = statePayrollConceptDepartment;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static PayrollConceptDepartment Create(
        long? payrollConceptId, 
        long? departmentId, 
        int statePayrollConceptDepartment, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new PayrollConceptDepartment(
            payrollConceptId,
            departmentId,
            statePayrollConceptDepartment,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        long? payrollConceptId, 
        long? departmentId, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        PayrollConceptId = payrollConceptId;
        DepartmentId = departmentId;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StatePayrollConceptDepartment = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
