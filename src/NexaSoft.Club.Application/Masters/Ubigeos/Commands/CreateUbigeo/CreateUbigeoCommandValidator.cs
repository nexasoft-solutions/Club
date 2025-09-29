using FluentValidation;

namespace NexaSoft.Club.Application.Masters.Ubigeos.Commands.CreateUbigeo;

public class CreateUbigeoCommandValidator : AbstractValidator<CreateUbigeoCommand>
{
    public CreateUbigeoCommandValidator()
    {
        RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("El campo Descripcion no puede estar vacío.");
        /*RuleFor(x => x.PadreId)
            .NotEmpty().WithMessage("El identificador no puede estar vacío.");*/
        // Validación personalizada para Padre de tipo Ubigeo
    }
}
