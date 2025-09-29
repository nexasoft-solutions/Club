using FluentValidation;

namespace NexaSoft.Club.Application.Masters.MemberStatuses.Commands.CreateMemberStatus;

public class CreateMemberStatusCommandValidator : AbstractValidator<CreateMemberStatusCommand>
{
    public CreateMemberStatusCommandValidator()
    {
        RuleFor(x => x.StatusName)
            .NotEmpty().WithMessage("El campo StatusName no puede estar vacío.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
    }
}
