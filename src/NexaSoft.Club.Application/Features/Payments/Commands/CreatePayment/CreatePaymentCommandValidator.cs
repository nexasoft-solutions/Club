using FluentValidation;

namespace NexaSoft.Club.Application.Features.Payments.Commands.CreatePayment;

public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentCommandValidator()
    {
        // Validación personalizada para Member de tipo Member
        /*RuleFor(x => x.FeeId)
            .GreaterThan(0).WithMessage("Este FeeId debe ser mayor a cero.");*/
        // Validación personalizada para MemberFee de tipo MemberFee
        RuleFor(x => x.PaymentMethod)
            .NotEmpty().WithMessage("El campo PaymentMethod no puede estar vacío.");
        RuleFor(x => x.ReferenceNumber)
            .NotEmpty().WithMessage("El campo ReferenceNumber no puede estar vacío.");
        RuleFor(x => x.ReceiptNumber)
            .NotEmpty().WithMessage("El campo ReceiptNumber no puede estar vacío.");
        // Validación personalizada para AccountingEntry de tipo AccountingEntry
    }
}
