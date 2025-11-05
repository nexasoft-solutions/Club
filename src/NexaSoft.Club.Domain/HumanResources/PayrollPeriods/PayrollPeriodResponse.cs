namespace NexaSoft.Club.Domain.HumanResources.PayrollPeriods;

public sealed record PayrollPeriodResponse(
    long Id,
    string? PeriodName,
    long? PayrollTypeId,
    string? PayrollTypeCode,
    DateOnly? StartDate,
    DateOnly? EndDate,
    decimal TotalAmount,
    int? TotalEmployees,
    long? StatusId,
    string? StatusCode,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy,
    List<PayrollDetailItemResponse> Details
);

/*public sealed record PayrollPeriodWithDetailsResponse(
    long Id,
    string? PeriodName,
    long? PayrollTypeId,
    string? PayrollTypeCode,
    DateOnly? StartDate,
    DateOnly? EndDate,
    decimal TotalAmount,
    int? TotalEmployees,
    DateTime CreatedAt,
    string? CreatedBy,
    IEnumerable<PayrollDetailItemResponse> Details
);*/

public sealed record PayrollDetailItemResponse(
    long Id,
    long? EmployeeId,
    string? EmployeeCode,
    decimal BaseSalary,
    decimal TotalIncome,
    decimal TotalDeductions,
    decimal NetPay
);

// Conceptos de un detalle de planilla x Empleado
public sealed record PayrollPeriodItemResponse(
    long Id,
    string? PeriodName,
    long? PayrollTypeId,
    string? PayrollTypeCode,
    DateOnly? StartDate,
    DateOnly? EndDate,
    decimal TotalAmount,
    int? TotalEmployees,
    long? StatusId,
    DateTime CreatedAt,
    string? CreatedBy,
    List<PayrollDetailItemsResponse> Details
);

public record PayrollDetailItemsResponse(
    long Id,
    string? PeriodNameDetail,
    DateOnly? CreatedAtDetail,
    long EmployeeId,
    string EmployeeCode,
    string EmployeeFullName,
    string EmployeeDni,
    string EmployeePositionCode,
    string EmployeeDepartmentCode,
    string ContractType,
    DateOnly? EmployeeHireDate,
    decimal BaseSalary,
    decimal TotalIncome,
    decimal TotalDeductions,
    decimal NetPay,
    List<PayrollDetailConceptItemsResponse>? Concepts = null
);

public sealed record PayrollDetailConceptItemsResponse(
    long Id,
    long ConceptId,
    string ConceptCode,
    string ConceptName,
    string ConceptType, 
    decimal Amount,
    decimal Quantity,
    decimal CalculatedValue,
    string? Description
);
