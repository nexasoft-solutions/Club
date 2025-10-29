using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriods.Commands.CreatePayrollPeriod;

public class CreatePayrollPeriodCommandValidator : AbstractValidator<CreatePayrollPeriodCommand>
{
    public CreatePayrollPeriodCommandValidator()
    {
        RuleFor(x => x.PeriodName)
            .NotEmpty().WithMessage("El campo PeriodName no puede estar vacío.");
        // Validación personalizada para StartDate de tipo DateOnly
        // Validación personalizada para EndDate de tipo DateOnly
        RuleFor(x => x.TotalEmployees)
            .GreaterThan(0).WithMessage("Este TotalEmployees debe ser mayor a cero.");
        RuleFor(x => x.StatusId)
            .GreaterThan(0).WithMessage("Este StatusId debe ser mayor a cero.");
        // Validación personalizada para Status de tipo Status
    }
}
