using FluentValidation;

namespace NexaSoft.Club.Application.Features.FamilyMembers.Commands.CreateFamilyMember;

public class CreateFamilyMemberCommandValidator : AbstractValidator<CreateFamilyMemberCommand>
{
    public CreateFamilyMemberCommandValidator()
    {
        // Validación personalizada para Member de tipo Member
        RuleFor(x => x.Dni)
            .NotEmpty().WithMessage("El campo Dni no puede estar vacío.");
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("El campo FirstName no puede estar vacío.");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("El campo LastName no puede estar vacío.");
        RuleFor(x => x.Relationship)
            .NotEmpty().WithMessage("El campo Relationship no puede estar vacío.");
        // Validación personalizada para BirthDate de tipo DateOnly
    }
}
