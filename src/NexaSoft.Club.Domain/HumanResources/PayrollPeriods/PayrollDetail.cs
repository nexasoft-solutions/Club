using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.CostCenters;
using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;
using NexaSoft.Club.Domain.Masters.Statuses;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.HumanResources.PayrollPeriods;

public class PayrollDetail: Entity
{
    public long PayrollPeriodId { get; private set; }
    public long EmployeeId { get; private set; }
    public long? CostCenterId { get; private set; }

    public decimal? BaseSalary { get; private set; }
    public decimal? TotalIncome { get; private set; }
    public decimal? TotalDeductions { get; private set; }
    public decimal? NetPay { get; private set; }

    public long? StatusId { get; private set; }
    public DateOnly? CalculatedAt { get; private set; }
    public DateOnly? PaidAt { get; private set; }

    public PayrollPeriod? PayrollPeriod { get; private set; }
    public EmployeeInfo? Employee { get; private set; }
    public CostCenter? CostCenter { get; private set; }
    public Status? Status { get; private set; }

    public int StatePayrollDetail { get; private set; }

    private PayrollDetail() { }

    private PayrollDetail(
        long payrollPeriodId,
        long employeeId,
        long? costCenterId,
        decimal? baseSalary,
        decimal? totalIncome,
        decimal? totalDeductions,
        decimal? netPay,
        long? statusId,
        int statePayrollDetail,
        DateTime createdAt,
        string? createdBy     
    ) : base(createdAt, createdBy)
    {
        PayrollPeriodId = payrollPeriodId;
        EmployeeId = employeeId;
        CostCenterId = costCenterId;
        BaseSalary = baseSalary;
        TotalIncome = totalIncome;
        TotalDeductions = totalDeductions;
        NetPay = netPay;
        StatusId = statusId;        
        StatePayrollDetail = statePayrollDetail;
    }

    public static PayrollDetail Create(
        long payrollPeriodId,
        long employeeId,
        long? costCenterId,
        decimal? baseSalary,
        decimal? totalIncome,
        decimal? totalDeductions,
        decimal? netPay,
        long? statusId,
        int statePayrollDetail,
        DateTime createdAt,
        string? createdBy
    )
    {
        return new PayrollDetail(
            payrollPeriodId,
            employeeId,
            costCenterId,
            baseSalary,
            totalIncome,
            totalDeductions,
            netPay,
            statusId,
            statePayrollDetail,
            createdAt,
            createdBy
        );
    }

    /*public Result Update(
        decimal? baseSalary,
        decimal? totalIncome,
        decimal? totalDeductions,
        decimal? netPay,
        long? statusId,
        DateTime utcNow,
        string? updatedBy
    )
    {
        BaseSalary = baseSalary;
        TotalIncome = totalIncome;
        TotalDeductions = totalDeductions;
        NetPay = netPay;
        StatusId = statusId;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;
        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string deletedBy)
    {
        StatePayrollDetail = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }*/
}
