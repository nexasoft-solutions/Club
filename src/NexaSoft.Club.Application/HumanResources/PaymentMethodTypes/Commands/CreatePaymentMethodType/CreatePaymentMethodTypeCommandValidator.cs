using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.PaymentMethodTypes.Commands.CreatePaymentMethodType;

public class CreatePaymentMethodTypeCommandValidator : AbstractValidator<CreatePaymentMethodTypeCommand>
{
    public CreatePaymentMethodTypeCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El campo Code no puede estar vacío.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo Name no puede estar vacío.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
    }
}
