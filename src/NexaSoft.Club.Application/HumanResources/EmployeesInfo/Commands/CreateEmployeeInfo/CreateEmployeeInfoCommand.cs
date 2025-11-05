using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.EmployeesInfo.Commands.CreateEmployeeInfo;

public sealed record CreateEmployeeInfoCommand(
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
) : ICommand<long>;
