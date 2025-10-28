using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.Banks.Commands.CreateBank;

public class CreateBankCommandValidator : AbstractValidator<CreateBankCommand>
{
    public CreateBankCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El campo Code no puede estar vacío.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo Name no puede estar vacío.");
        RuleFor(x => x.WebSite)
            .NotEmpty().WithMessage("El campo WebSite no puede estar vacío.");
        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("El campo Phone no puede estar vacío.");
    }
}
