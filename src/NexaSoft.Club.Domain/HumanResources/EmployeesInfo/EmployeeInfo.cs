using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.Masters.Users;
using NexaSoft.Club.Domain.HumanResources.Positions;
using NexaSoft.Club.Domain.HumanResources.EmployeeTypes;
using NexaSoft.Club.Domain.HumanResources.Departments;
using NexaSoft.Club.Domain.HumanResources.PaymentMethodTypes;
using NexaSoft.Club.Domain.HumanResources.Banks;
using NexaSoft.Club.Domain.HumanResources.BankAccountTypes;
using NexaSoft.Club.Domain.HumanResources.Currencies;
using NexaSoft.Club.Domain.HumanResources.Companies;

namespace NexaSoft.Club.Domain.HumanResources.EmployeesInfo;

public class EmployeeInfo : Entity
{
    public string? EmployeeCode { get; private set; }
    public long? UserId { get; private set; }
    public User? User { get; private set; }
    public long? PositionId { get; private set; }
    public Position? Position { get; private set; }
    public long? EmployeeTypeId { get; private set; }
    public EmployeeType? EmployeeType { get; private set; }
    public long? DepartmentId { get; private set; }
    public Department? Department { get; private set; }
    public DateOnly HireDate { get; private set; }
    public decimal BaseSalary { get; private set; }
    public long? PaymentMethodId { get; private set; }
    public PaymentMethodType? PaymentMethodType { get; private set; }
    public long? BankId { get; private set; }
    public Bank? Bank { get; private set; }
    public long? BankAccountTypeId { get; private set; }
    public BankAccountType? BankAccountType { get; private set; }
    public long? CurrencyId { get; private set; }
    public Currency? Currency { get; private set; }
    public string? BankAccountNumber { get; private set; }
    public string? CciNumber { get; private set; }
    public int StateEmployeeInfo { get; private set; }
    public long? CompanyId { get; private set; }
    public Company? Company { get; set; }

    private EmployeeInfo() { }

    private EmployeeInfo(
        string? employeeCode,
        long? userId,
        long? positionId,
        long? employeeTypeId,
        long? departmentId,
        DateOnly hireDate,
        decimal baseSalary,
        long? paymentMethodId,
        long? bankId,
        long? bankAccountTypeId,
        long? currencyId,
        string? bankAccountNumber,
        string? cciNumber,
        long? companyId,
        int stateEmployeeInfo,
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        EmployeeCode = employeeCode;
        UserId = userId;
        PositionId = positionId;
        EmployeeTypeId = employeeTypeId;
        DepartmentId = departmentId;
        HireDate = hireDate;
        BaseSalary = baseSalary;
        PaymentMethodId = paymentMethodId;
        BankId = bankId;
        BankAccountTypeId = bankAccountTypeId;
        CurrencyId = currencyId;
        BankAccountNumber = bankAccountNumber;
        CciNumber = cciNumber;
        CompanyId = companyId;
        StateEmployeeInfo = stateEmployeeInfo;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static EmployeeInfo Create(
        string? employeeCode,
        long? userId,
        long? positionId,
        long? employeeTypeId,
        long? departmentId,
        DateOnly hireDate,
        decimal baseSalary,
        long? paymentMethodId,
        long? bankId,
        long? bankAccountTypeId,
        long? currencyId,
        string? bankAccountNumber,
        string? cciNumber,
        long? companyId,
        int stateEmployeeInfo,
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new EmployeeInfo(
            employeeCode,
            userId,
            positionId,
            employeeTypeId,
            departmentId,
            hireDate,
            baseSalary,
            paymentMethodId,
            bankId,
            bankAccountTypeId,
            currencyId,
            bankAccountNumber,
            cciNumber,
            companyId,
            stateEmployeeInfo,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? employeeCode,
        long? userId,
        long? positionId,
        long? employeeTypeId,
        long? departmentId,
        DateOnly hireDate,
        decimal baseSalary,
        long? paymentMethodId,
        long? bankId,
        long? bankAccountTypeId,
        long? currencyId,
        string? bankAccountNumber,
        string? cciNumber,
        long? companyId,
        DateTime utcNow,
        string? updatedBy
    )
    {
        EmployeeCode = employeeCode;
        UserId = userId;
        PositionId = positionId;
        EmployeeTypeId = employeeTypeId;
        DepartmentId = departmentId;
        HireDate = hireDate;
        BaseSalary = baseSalary;
        PaymentMethodId = paymentMethodId;
        BankId = bankId;
        BankAccountTypeId = bankAccountTypeId;
        CurrencyId = currencyId;
        BankAccountNumber = bankAccountNumber;
        CciNumber = cciNumber;
        CompanyId = companyId;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateEmployeeInfo = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
