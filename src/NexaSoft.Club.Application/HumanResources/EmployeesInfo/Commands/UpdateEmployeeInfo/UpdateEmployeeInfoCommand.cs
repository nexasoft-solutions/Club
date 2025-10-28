using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.EmployeesInfo.Commands.UpdateEmployeeInfo;

public sealed record UpdateEmployeeInfoCommand(
    long Id,
    string? EmployeeCode,
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
    string? Cci_Number,
    string UpdatedBy
) : ICommand<bool>;
