using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.SpecialRates.Commands.CreateSpecialRate;

public class CreateSpecialRateCommandValidator : AbstractValidator<CreateSpecialRateCommand>
{
    public CreateSpecialRateCommandValidator()
    {
        RuleFor(x => x.RateTypeId)
            .GreaterThan(0).WithMessage("Este RateTypeId debe ser mayor a cero.");
        // Validación personalizada para RateType de tipo RateType
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo Name no puede estar vacío.");
        // Validación personalizada para StartTime de tipo TimeOnly
        // Validación personalizada para EndTime de tipo TimeOnly
    }
}
