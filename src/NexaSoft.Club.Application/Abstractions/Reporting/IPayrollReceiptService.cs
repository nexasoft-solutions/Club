using NexaSoft.Club.Application.HumanResources.PayrollPeriods.Queries.GetReceiptPeriodByEmployee;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriods;

namespace NexaSoft.Club.Application.Abstractions.Reporting;

public interface IPayrollReceiptService
{
    byte[] GeneratePayrollReceipt(PayrollPeriodItemResponse payroll, Action<PayrollReceiptConfig>? configure = null);
    byte[] GenerateEmployeePayrollReceipt(PayrollDetailItemsResponse employeeDetail, Action<PayrollReceiptConfig>? configure = null);
    byte[] GenerateA4PayrollReceipt(PayrollPeriodItemResponse payroll);
    byte[] GenerateA5PayrollReceipt(PayrollPeriodItemResponse payroll);
    PayrollReceiptConfig GetDefaultConfiguration();
}
