using FluentValidation;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Commands.CreateOrganizacion;

public class CreateOrganizacionCommandValidator : AbstractValidator<CreateOrganizacionCommand>
{
    public CreateOrganizacionCommandValidator()
    {
        RuleFor(x => x.NombreOrganizacion)
            .NotEmpty().WithMessage("El campo NombreOrganizacion no puede estar vacío.");
        RuleFor(x => x.ContactoOrganizacion)
            .NotEmpty().WithMessage("El campo ContactoOrganizacion no puede estar vacío.");
        RuleFor(x => x.TelefonoContacto)
            .NotEmpty().WithMessage("El campo TelefonoContacto no puede estar vacío.");
    }
}
