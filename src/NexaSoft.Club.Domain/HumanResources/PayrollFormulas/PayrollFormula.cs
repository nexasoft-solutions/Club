using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.HumanResources.PayrollFormulas;

public class PayrollFormula : Entity
{
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public string? FormulaExpression { get; private set; }
    public string? Description { get; private set; }
    public string? Variables { get; private set; }
    public int StatePayrollFormula { get; private set; }

    private PayrollFormula() { }

    private PayrollFormula(
        string? code, 
        string? name, 
        string? formulaExpression, 
        string? description, 
        string? variables, 
        int statePayrollFormula, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Code = code;
        Name = name;
        FormulaExpression = formulaExpression;
        Description = description;
        Variables = variables;
        StatePayrollFormula = statePayrollFormula;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static PayrollFormula Create(
        string? code, 
        string? name, 
        string? formulaExpression, 
        string? description, 
        string? variables, 
        int statePayrollFormula, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new PayrollFormula(
            code,
            name,
            formulaExpression,
            description,
            variables,
            statePayrollFormula,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? code, 
        string? name, 
        string? formulaExpression, 
        string? description, 
        string? variables, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        Code = code;
        Name = name;
        FormulaExpression = formulaExpression;
        Description = description;
        Variables = variables;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StatePayrollFormula = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
