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
       
    }
}
