using FluentValidation;

namespace NexaSoft.Club.Application.Masters.SpaceAvailabilities.Commands.CreateSpaceAvailability;

public class CreateSpaceAvailabilityCommandValidator : AbstractValidator<CreateSpaceAvailabilityCommand>
{
    public CreateSpaceAvailabilityCommandValidator()
    {
        // Validación personalizada para Space de tipo Space
        RuleFor(x => x.StartTime)
            .GreaterThan(TimeSpan.Zero).WithMessage("Este StartTime debe ser mayor a cero.");

        RuleFor(x => x.EndTime)
            .GreaterThan(TimeSpan.Zero).WithMessage("Este EndTime debe ser mayor a cero.");
        
        
    }
}
