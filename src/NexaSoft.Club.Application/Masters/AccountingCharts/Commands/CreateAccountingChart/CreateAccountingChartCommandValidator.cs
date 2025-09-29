using FluentValidation;

namespace NexaSoft.Club.Application.Masters.AccountingCharts.Commands.CreateAccountingChart;

public class CreateAccountingChartCommandValidator : AbstractValidator<CreateAccountingChartCommand>
{
    public CreateAccountingChartCommandValidator()
    {
        RuleFor(x => x.AccountCode)
            .NotEmpty().WithMessage("El campo AccountCode no puede estar vacío.");
        RuleFor(x => x.AccountName)
            .NotEmpty().WithMessage("El campo AccountName no puede estar vacío.");
        RuleFor(x => x.AccountType)
            .NotEmpty().WithMessage("El campo AccountType no puede estar vacío.");
        RuleFor(x => x.ParentAccountId)
            .GreaterThan(0).WithMessage("Este ParentAccountId debe ser mayor a cero.");
        // Validación personalizada para ParentAccount de tipo AccountingChart
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
    }
}
