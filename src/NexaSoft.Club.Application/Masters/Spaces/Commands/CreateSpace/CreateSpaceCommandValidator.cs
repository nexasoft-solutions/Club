using FluentValidation;

namespace NexaSoft.Club.Application.Masters.Spaces.Commands.CreateSpace;

public class CreateSpaceCommandValidator : AbstractValidator<CreateSpaceCommand>
{
    public CreateSpaceCommandValidator()
    {
        RuleFor(x => x.SpaceName)
            .NotEmpty().WithMessage("El campo SpaceName no puede estar vacío.");
        RuleFor(x => x.SpaceType)
            .NotEmpty().WithMessage("El campo SpaceType no puede estar vacío.");
        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("Este Capacity debe ser mayor a cero.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
        RuleFor(x => x.IncomeAccountId)
            .GreaterThan(0).WithMessage("Este IncomeAccountId debe ser mayor a cero.");
        // Validación personalizada para IncomeAccount de tipo AccountingChart
    }
}
