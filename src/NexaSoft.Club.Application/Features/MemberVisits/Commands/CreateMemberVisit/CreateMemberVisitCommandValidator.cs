using FluentValidation;

namespace NexaSoft.Club.Application.Features.MemberVisits.Commands.CreateMemberVisit;

public class CreateMemberVisitCommandValidator : AbstractValidator<CreateMemberVisitCommand>
{
    public CreateMemberVisitCommandValidator()
    {
        RuleFor(x => x.MemberId)
            .GreaterThan(0).WithMessage("Este MemberId debe ser mayor a cero.");
        // Validación personalizada para Member de tipo Member
        // Validación personalizada para VisitDate de tipo DateOnly
        // Validación personalizada para EntryTime de tipo TimeOnly
        // Validación personalizada para ExitTime de tipo TimeOnly
        RuleFor(x => x.QrCodeUsed)
            .NotEmpty().WithMessage("El campo QrCodeUsed no puede estar vacío.");
        RuleFor(x => x.Notes)
            .NotEmpty().WithMessage("El campo Notes no puede estar vacío.");
          
    }
}
