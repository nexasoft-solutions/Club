using FluentValidation;

namespace NexaSoft.Club.Application.Masters.MemberTypes.Commands.CreateMemberType;

public class CreateMemberTypeCommandValidator : AbstractValidator<CreateMemberTypeCommand>
{
    public CreateMemberTypeCommandValidator()
    {
        RuleFor(x => x.TypeName)
            .NotEmpty().WithMessage("El campo TypeName no puede estar vacío.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
        /*RuleFor(x => x.IncomeAccountId)
            .GreaterThan(0).WithMessage("Este IncomeAccountId debe ser mayor a cero.");*/
        // Validación personalizada para IncomeAccount de tipo AccountingChart
    }
}
