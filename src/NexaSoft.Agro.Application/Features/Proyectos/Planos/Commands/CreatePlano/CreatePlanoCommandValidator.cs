using FluentValidation;

namespace NexaSoft.Agro.Application.Features.Proyectos.Planos.Commands.CreatePlano;

public class CreatePlanoCommandValidator : AbstractValidator<CreatePlanoCommand>
{
    public CreatePlanoCommandValidator()
    {
        RuleFor(x => x.SistemaProyeccion)
            .NotEmpty().WithMessage("El campo SistemaProyeccion no puede estar vacío.");
        RuleFor(x => x.NombrePlano)
            .NotEmpty().WithMessage("El campo NombrePlano no puede estar vacío.");
        // Validación personalizada para Archivo de tipo Archivo
        // Validación personalizada para Colaborador de tipo Colaborador
    }
}
