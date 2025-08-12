using FluentValidation;

namespace NexaSoft.Agro.Application.Masters.Consultoras.Colaboradores.Commands.CreateColaborador;

public class CreateColaboradorCommandValidator : AbstractValidator<CreateColaboradorCommand>
{
    public CreateColaboradorCommandValidator()
    {
        RuleFor(x => x.NombresColaborador)
            .NotEmpty().WithMessage("El campo NombresColaborador no puede estar vacío.");
        RuleFor(x => x.ApellidosColaborador)
            .NotEmpty().WithMessage("El campo ApellidosColaborador no puede estar vacío.");
        RuleFor(x => x.NumeroDocumentoIdentidad)
            .NotEmpty().WithMessage("El campo NumeroDocumentoIdentidad no puede estar vacío.");
        // Validación personalizada para FechaNacimiento de tipo DateOnly
        RuleFor(x => x.Direccion)
            .NotEmpty().WithMessage("El campo Direccion no puede estar vacío.");
        RuleFor(x => x.CorreoElectronico)
            .NotEmpty().WithMessage("El campo CorreoElectronico no puede estar vacío.");
        RuleFor(x => x.TelefonoMovil)
            .NotEmpty().WithMessage("El campo TelefonoMovil no puede estar vacío.");
        // Validación personalizada para FechaIngreso de tipo DateOnly
        // Validación personalizada para Salario de tipo decimal
        // Validación personalizada para FechaCese de tipo DateOnly
        RuleFor(x => x.Comentarios)
            .NotEmpty().WithMessage("El campo Comentarios no puede estar vacío.");
        // Validación personalizada para Consultora de tipo Consultora
    }
}
