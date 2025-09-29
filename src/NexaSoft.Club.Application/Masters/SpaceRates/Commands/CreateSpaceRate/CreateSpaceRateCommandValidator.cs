using FluentValidation;

namespace NexaSoft.Club.Application.Masters.SpaceRates.Commands.CreateSpaceRate;

public class CreateSpaceRateCommandValidator : AbstractValidator<CreateSpaceRateCommand>
{
    public CreateSpaceRateCommandValidator()
    {
        // Validación personalizada para Space de tipo Space
        // Validación personalizada para MemberType de tipo MemberType
    }
}
