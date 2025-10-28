using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.BankAccountTypes.Commands.CreateBankAccountType;

public class CreateBankAccountTypeCommandValidator : AbstractValidator<CreateBankAccountTypeCommand>
{
    public CreateBankAccountTypeCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El campo Code no puede estar vacío.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo Name no puede estar vacío.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
    }
}
