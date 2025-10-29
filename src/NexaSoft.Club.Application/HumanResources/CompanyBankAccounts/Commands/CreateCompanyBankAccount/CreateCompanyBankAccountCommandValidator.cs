using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.CompanyBankAccounts.Commands.CreateCompanyBankAccount;

public class CreateCompanyBankAccountCommandValidator : AbstractValidator<CreateCompanyBankAccountCommand>
{
    public CreateCompanyBankAccountCommandValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("Este CompanyId debe ser mayor a cero.");
        // Validación personalizada para Company de tipo Company
        RuleFor(x => x.BankId)
            .GreaterThan(0).WithMessage("Este BankId debe ser mayor a cero.");
        // Validación personalizada para Bank de tipo Bank
        RuleFor(x => x.BankAccountTypeId)
            .GreaterThan(0).WithMessage("Este BankAccountTypeId debe ser mayor a cero.");
        // Validación personalizada para BankAccountType de tipo BankAccountType
        RuleFor(x => x.BankAccountNumber)
            .NotEmpty().WithMessage("El campo BankAccountNumber no puede estar vacío.");
        RuleFor(x => x.CciNumber)
            .NotEmpty().WithMessage("El campo CciNumber no puede estar vacío.");
        RuleFor(x => x.CurrencyId)
            .GreaterThan(0).WithMessage("Este CurrencyId debe ser mayor a cero.");
        // Validación personalizada para Currency de tipo Currency
    }
}
