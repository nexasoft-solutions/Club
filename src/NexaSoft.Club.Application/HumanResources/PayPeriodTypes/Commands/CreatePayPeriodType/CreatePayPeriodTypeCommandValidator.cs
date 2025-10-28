using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.PayPeriodTypes.Commands.CreatePayPeriodType;

public class CreatePayPeriodTypeCommandValidator : AbstractValidator<CreatePayPeriodTypeCommand>
{
    public CreatePayPeriodTypeCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El campo Code no puede estar vacío.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo Name no puede estar vacío.");
        RuleFor(x => x.Days)
            .GreaterThan(0).WithMessage("Este Days debe ser mayor a cero.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
    }
}
