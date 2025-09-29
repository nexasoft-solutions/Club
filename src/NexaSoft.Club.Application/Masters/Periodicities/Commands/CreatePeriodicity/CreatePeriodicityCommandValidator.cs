using FluentValidation;

namespace NexaSoft.Club.Application.Masters.Periodicities.Commands.CreatePeriodicity;

public class CreatePeriodicityCommandValidator : AbstractValidator<CreatePeriodicityCommand>
{
    public CreatePeriodicityCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo Name no puede estar vacío.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
    }
}
