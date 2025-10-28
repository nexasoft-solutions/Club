using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.AttendanceStatusTypes.Commands.CreateAttendanceStatusType;

public class CreateAttendanceStatusTypeCommandValidator : AbstractValidator<CreateAttendanceStatusTypeCommand>
{
    public CreateAttendanceStatusTypeCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El campo Code no puede estar vacío.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo Name no puede estar vacío.");
        // Validación personalizada para IsPaid de tipo bool
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
    }
}
