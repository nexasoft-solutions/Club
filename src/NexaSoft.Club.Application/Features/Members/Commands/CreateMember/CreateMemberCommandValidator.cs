using FluentValidation;

namespace NexaSoft.Club.Application.Features.Members.Commands.CreateMember;

public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
{
    public CreateMemberCommandValidator()
    {
        RuleFor(x => x.Dni)
            .NotEmpty().WithMessage("El campo Dni no puede estar vacío.");
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("El campo FirstName no puede estar vacío.");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("El campo LastName no puede estar vacío.");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El campo Email no puede estar vacío.");
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("El campo Email no es una dirección de correo electrónico válida.");
        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("El campo Phone no puede estar vacío.");
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("El campo Address no puede estar vacío.");     
        //RuleFor(x => x.ProfilePictureUrl)
        //    .NotEmpty().WithMessage("El campo ProfilePictureUrl no puede estar vacío.");
    }
}
