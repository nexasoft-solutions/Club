using FluentValidation;

namespace NexaSoft.Club.Application.Features.MemberFees.Commands.CreateMemberFee;

public class CreateMemberFeeCommandValidator : AbstractValidator<CreateMemberFeeCommand>
{
    public CreateMemberFeeCommandValidator()
    {
        // Validación personalizada para Member de tipo Member
        // Validación personalizada para FeeConfiguration de tipo FeeConfiguration
        RuleFor(x => x.Period)
            .NotEmpty().WithMessage("El campo Period no puede estar vacío.");
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("El campo Status no puede estar vacío.");
    }
}
