using FluentValidation;

namespace NexaSoft.Club.Application.Features.Reservations.Commands.CreateReservation;

public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {
        // Validación personalizada para Member de tipo Member
        // Validación personalizada para Space de tipo Space
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("El campo Status no puede estar vacío.");
        RuleFor(x => x.AccountingEntryId)
            .GreaterThan(0).WithMessage("Este AccountingEntryId debe ser mayor a cero.");
        // Validación personalizada para AccountingEntry de tipo AccountingEntry
    }
}
