using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.Companies.Commands.CreateCompany;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(x => x.Ruc)
            .NotEmpty().WithMessage("El campo Ruc no puede estar vacío.");
        RuleFor(x => x.BusinessName)
            .NotEmpty().WithMessage("El campo BusinessName no puede estar vacío.");
        RuleFor(x => x.TradeName)
            .NotEmpty().WithMessage("El campo TradeName no puede estar vacío.");
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("El campo Address no puede estar vacío.");
        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("El campo Phone no puede estar vacío.");
        RuleFor(x => x.Website)
            .NotEmpty().WithMessage("El campo Website no puede estar vacío.");
    }
}
