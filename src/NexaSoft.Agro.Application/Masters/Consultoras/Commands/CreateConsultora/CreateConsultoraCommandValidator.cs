using FluentValidation;

namespace NexaSoft.Agro.Application.Masters.Consultoras.Commands.CreateConsultora;

public class CreateConsultoraCommandValidator : AbstractValidator<CreateConsultoraCommand>
{
    public CreateConsultoraCommandValidator()
    {
        RuleFor(x => x.NombreConsultora)
            .NotEmpty().WithMessage("El campo NombreConsultora no puede estar vacío.");
        RuleFor(x => x.DireccionConsultora)
            .NotEmpty().WithMessage("El campo DireccionConsultora no puede estar vacío.");
        RuleFor(x => x.RepresentanteConsultora)
            .NotEmpty().WithMessage("El campo RepresentanteConsultora no puede estar vacío.");
        RuleFor(x => x.RucConsultora)
            .NotEmpty().WithMessage("El campo RucConsultora no puede estar vacío.");
        RuleFor(x => x.CorreoOrganizacional)
            .NotEmpty().WithMessage("El campo CorreoOrganizacional no puede estar vacío.");
    }
}
