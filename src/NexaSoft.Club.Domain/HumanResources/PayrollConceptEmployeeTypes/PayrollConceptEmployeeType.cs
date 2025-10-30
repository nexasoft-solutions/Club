using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.HumanResources.PayrollConcepts;
using NexaSoft.Club.Domain.HumanResources.EmployeeTypes;

namespace NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployeeTypes;

public class PayrollConceptEmployeeType : Entity
{
    public long? PayrollConceptId { get; private set; }
    public PayrollConcept? PayrollConcept { get; private set; }
    public long? EmployeeTypeId { get; private set; }
    public EmployeeType? EmployeeType { get; private set; }
    public int StatePayrollConceptEmployeeType { get; private set; }

    private PayrollConceptEmployeeType() { }

    private PayrollConceptEmployeeType(
        long? payrollConceptId, 
        long? employeeTypeId, 
        int statePayrollConceptEmployeeType, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        PayrollConceptId = payrollConceptId;
        EmployeeTypeId = employeeTypeId;
        StatePayrollConceptEmployeeType = statePayrollConceptEmployeeType;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static PayrollConceptEmployeeType Create(
        long? payrollConceptId, 
        long? employeeTypeId, 
        int statePayrollConceptEmployeeType, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new PayrollConceptEmployeeType(
            payrollConceptId,
            employeeTypeId,
            statePayrollConceptEmployeeType,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        long? payrollConceptId, 
        long? employeeTypeId, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        PayrollConceptId = payrollConceptId;
        EmployeeTypeId = employeeTypeId;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StatePayrollConceptEmployeeType = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
