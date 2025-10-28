using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.PayrollStatusTypes.Commands.CreatePayrollStatusType;

public class CreatePayrollStatusTypeCommandValidator : AbstractValidator<CreatePayrollStatusTypeCommand>
{
    public CreatePayrollStatusTypeCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El campo Code no puede estar vacío.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo Name no puede estar vacío.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
    }
}
