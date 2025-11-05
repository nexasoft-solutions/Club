namespace NexaSoft.Club.Api.Controllers.HumanResources.EmployeesInfo.Request;

public sealed record CreateEmployeeInfoRequest(
    long? UserId,
    long? PositionId,
    long? EmployeeTypeId,
    long? DepartmentId,
    DateOnly HireDate,
    decimal BaseSalary,
    long? PaymentMethodId,
    long? BankId,
    long? BankAccountTypeId,
    long? CurrencyId,
    string? BankAccountNumber,
    string? CciNumber,
    long? CompanyId,
    long? CostCenterId,
    bool? IsFamilyAllowance,
    string CreatedBy
);
