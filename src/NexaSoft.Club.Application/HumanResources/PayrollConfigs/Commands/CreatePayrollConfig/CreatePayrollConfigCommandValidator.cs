using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.PayrollConfigs.Commands.CreatePayrollConfig;

public class CreatePayrollConfigCommandValidator : AbstractValidator<CreatePayrollConfigCommand>
{
    public CreatePayrollConfigCommandValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("Este CompanyId debe ser mayor a cero.");
        // Validación personalizada para Company de tipo Company
        RuleFor(x => x.PayPeriodTypeId)
            .GreaterThan(0).WithMessage("Este PayPeriodTypeId debe ser mayor a cero.");
        // Validación personalizada para PayPeriodType de tipo PayPeriodType
    }
}
