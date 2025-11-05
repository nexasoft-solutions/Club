using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriods.Commands.CreatePayrollPeriod;

public class CreatePayrollPeriodCommandValidator : AbstractValidator<CreatePayrollPeriodCommand>
{
    public CreatePayrollPeriodCommandValidator()
    {
        /*RuleFor(x => x.PeriodName)
            .NotEmpty().WithMessage("El campo PeriodName no puede estar vacío.");*/
        // Validación personalizada para StartDate de tipo DateOnly

        RuleFor(x => x.PayrollTypeId)
            .NotNull().WithMessage("El campo PayrollTypeId no puede estar vacío.")
            .GreaterThan(0).WithMessage("El campo PayrollTypeId debe ser mayor que cero.");

        RuleFor(x => x.StartDate)
            .NotNull().WithMessage("El campo StartDate no puede estar vacío.");
            //.Must(BeAValidDate).WithMessage("El campo StartDate debe ser una fecha válida.");

        // Validación personalizada para EndDate de tipo DateOnly
        RuleFor(x => x.EndDate)
            .NotNull().WithMessage("El campo EndDate no puede estar vacío.")
            //.Must(BeAValidDate).WithMessage("El campo EndDate debe ser una fecha válida.")
            .GreaterThan(x => x.StartDate).WithMessage("El campo EndDate debe ser mayor que StartDate.");
    }
}
