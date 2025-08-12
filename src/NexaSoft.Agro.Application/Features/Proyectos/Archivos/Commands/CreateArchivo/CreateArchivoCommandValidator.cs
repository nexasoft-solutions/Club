using FluentValidation;

namespace NexaSoft.Agro.Application.Features.Proyectos.Archivos.Commands.CreateArchivo;

public class CreateArchivoCommandValidator : AbstractValidator<CreateArchivoCommand>
{
    public CreateArchivoCommandValidator()
    {
        /*RuleFor(x => x.NombreArchivo)
            .NotEmpty().WithMessage("El campo NombreArchivo no puede estar vacío.");*/
        RuleFor(x => x.DescripcionArchivo)
            .NotEmpty().WithMessage("El campo DescripcionArchivo no puede estar vacío.");
        /*RuleFor(x => x.RutaArchivo)
            .NotEmpty().WithMessage("El campo RutaArchivo no puede estar vacío.");*/
    
    }
}
