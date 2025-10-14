using FluentValidation;

namespace NexaSoft.Club.Application.Features.Reservations.Commands.CreateReservation;

public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {
        // Validación personalizada para Member de tipo Member
        // Validación personalizada para Space de tipo Space
        RuleFor(x => x.StatusId)
            .NotEmpty().WithMessage("El campo Status no puede estar vacío.");
     
        // Validación personalizada para AccountingEntry de tipo AccountingEntry
    }
}
