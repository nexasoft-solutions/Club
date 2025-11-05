using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriods.Commands.CreatePayrollPeriodPreview;

public sealed record CreatePayrollPeriodPreviewCommand(
    long? PayrollTypeId,
    DateOnly StartDate,
    DateOnly EndDate,
    string CreatedBy
) : ICommand<PayrollPreviewResponse>;

public sealed record PayrollPreviewResponse(
    string PeriodName,
    string PayrollTypeName,
    DateOnly StartDate,
    DateOnly EndDate,
    int TotalEmployees,
    decimal TotalAmount,
    List<EmployeePreviewRecord> Employees,
    List<ConceptSummaryRecord> ConceptSummary
);

public sealed record EmployeePreviewRecord(
    long EmployeeId,
    string EmployeeCode,
    string FullName,
    string Dni,
    string Department,
    decimal BaseSalary,
    List<ConceptDetailRecord> Concepts,
    decimal TotalIncome,
    decimal TotalDeductions,
    decimal NetPay
);

public sealed record ConceptDetailRecord(
    string ConceptCode,
    string ConceptName,
    string ConceptType,     // "INGRESO" o "DESCUENTO"
    decimal Amount,
    string AccountCode,
    string AccountName,
    string CalculationType  // "FIJO", "PORCENTAJE", "FORMULA"
);

public sealed record ConceptSummaryRecord(
    string ConceptCode,
    string ConceptName,
    string ConceptType,
    decimal TotalAmount,
    int AffectedEmployees
);

