using FluentValidation;

namespace NexaSoft.Club.Application.Masters.SpacePhotos.Commands.CreateSpacePhoto;

public class CreateSpacePhotoCommandValidator : AbstractValidator<CreateSpacePhotoCommand>
{
    public CreateSpacePhotoCommandValidator()
    {
        // Validación personalizada para Space de tipo Space
        /*RuleFor(x => x.PhotoUrl)
            .NotEmpty().WithMessage("El campo PhotoUrl no puede estar vacío.");*/
        RuleFor(x => x.SpaceId)
            .GreaterThan(0)
            .WithMessage("El SpaceId debe ser mayor a 0");

        RuleFor(x => x.PhotoFile)
            .NotNull()
            .WithMessage("El stream del archivo es requerido")
            .Must(BeAValidStream)
            .WithMessage("El stream debe ser válido y tener contenido");
            
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
    }
    private bool BeAValidStream(Stream stream)
    {
        return stream != null && stream.CanRead && stream.Length > 0;
    }

}
