using FluentValidation;

namespace NexaSoft.Club.Application.Masters.PaymentTypes.Commands.CreatePaymentType;

public class CreatePaymentTypeCommandValidator : AbstractValidator<CreatePaymentTypeCommand>
{
    public CreatePaymentTypeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo Name no puede estar vacío.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
    }
}
