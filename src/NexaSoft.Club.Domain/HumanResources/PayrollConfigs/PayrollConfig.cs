using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.HumanResources.Companies;
using NexaSoft.Club.Domain.HumanResources.PayPeriodTypes;

namespace NexaSoft.Club.Domain.HumanResources.PayrollConfigs;

public class PayrollConfig : Entity
{
    public long? CompanyId { get; private set; }
    public Company? Company { get; private set; }
    public long? PayPeriodTypeId { get; private set; }
    public PayPeriodType? PayPeriodType { get; private set; }
    public decimal RegularHoursPerDay { get; private set; }
    public int WorkDaysPerWeek { get; private set; }
    public int StatePayrollConfig { get; private set; }

    private PayrollConfig() { }

    private PayrollConfig(
        long? companyId, 
        long? payPeriodTypeId, 
        decimal regularHoursPerDay, 
        int workDaysPerWeek, 
        int statePayrollConfig, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        CompanyId = companyId;
        PayPeriodTypeId = payPeriodTypeId;
        RegularHoursPerDay = regularHoursPerDay;
        WorkDaysPerWeek = workDaysPerWeek;
        StatePayrollConfig = statePayrollConfig;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static PayrollConfig Create(
        long? companyId, 
        long? payPeriodTypeId, 
        decimal regularHoursPerDay, 
        int workDaysPerWeek, 
        int statePayrollConfig, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new PayrollConfig(
            companyId,
            payPeriodTypeId,
            regularHoursPerDay,
            workDaysPerWeek,
            statePayrollConfig,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        long? companyId, 
        long? payPeriodTypeId, 
        decimal regularHoursPerDay, 
        int workDaysPerWeek, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        CompanyId = companyId;
        PayPeriodTypeId = payPeriodTypeId;
        RegularHoursPerDay = regularHoursPerDay;
        WorkDaysPerWeek = workDaysPerWeek;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StatePayrollConfig = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
