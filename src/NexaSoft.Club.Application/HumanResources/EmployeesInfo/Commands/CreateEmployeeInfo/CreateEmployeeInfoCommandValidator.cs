using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.EmployeesInfo.Commands.CreateEmployeeInfo;

public class CreateEmployeeInfoCommandValidator : AbstractValidator<CreateEmployeeInfoCommand>
{
    public CreateEmployeeInfoCommandValidator()
    {
        RuleFor(x => x.EmployeeCode)
            .NotEmpty().WithMessage("El campo EmployeeCode no puede estar vacío.");
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("Este UserId debe ser mayor a cero.");
        // Validación personalizada para User de tipo User
        RuleFor(x => x.PositionId)
            .GreaterThan(0).WithMessage("Este PositionId debe ser mayor a cero.");
        // Validación personalizada para Position de tipo Position
        RuleFor(x => x.EmployeeTypeId)
            .GreaterThan(0).WithMessage("Este EmployeeTypeId debe ser mayor a cero.");
        // Validación personalizada para EmployeeType de tipo EmployeeType
        RuleFor(x => x.DepartmentId)
            .GreaterThan(0).WithMessage("Este DepartmentId debe ser mayor a cero.");
        // Validación personalizada para Department de tipo Department
        RuleFor(x => x.PaymentMethodId)
            .GreaterThan(0).WithMessage("Este PaymentMethodId debe ser mayor a cero.");
        // Validación personalizada para PaymentMethodType de tipo PaymentMethodType
        RuleFor(x => x.BankId)
            .GreaterThan(0).WithMessage("Este BankId debe ser mayor a cero.");
        // Validación personalizada para Bank de tipo Bank
        RuleFor(x => x.BankAccountTypeId)
            .GreaterThan(0).WithMessage("Este BankAccountTypeId debe ser mayor a cero.");
        // Validación personalizada para BankAccountType de tipo BankAccountType
        RuleFor(x => x.CurrencyId)
            .GreaterThan(0).WithMessage("Este CurrencyId debe ser mayor a cero.");
        // Validación personalizada para Currency de tipo Currency
        RuleFor(x => x.BankAccountNumber)
            .NotEmpty().WithMessage("El campo BankAccountNumber no puede estar vacío.");
      
    }
}
