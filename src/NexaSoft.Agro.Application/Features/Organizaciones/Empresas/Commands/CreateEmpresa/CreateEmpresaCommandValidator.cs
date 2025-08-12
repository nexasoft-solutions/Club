using FluentValidation;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Empresas.Commands.CreateEmpresa;

public class CreateEmpresaCommandValidator : AbstractValidator<CreateEmpresaCommand>
{
    public CreateEmpresaCommandValidator()
    {
        RuleFor(x => x.RazonSocial)
            .NotEmpty().WithMessage("El campo RazonSocial no puede estar vacío.");
        RuleFor(x => x.RucEmpresa)
            .NotEmpty().WithMessage("El campo RucEmpresa no puede estar vacío.");
        RuleFor(x => x.ContactoEmpresa)
            .NotEmpty().WithMessage("El campo ContactoEmpresa no puede estar vacío.");
        RuleFor(x => x.TelefonoContactoEmpresa)
            .NotEmpty().WithMessage("El campo TelefonoContactoEmpresa no puede estar vacío.");
        // Validación personalizada para DepartamentoEmpresa de tipo Ubigeo
        // Validación personalizada para ProvinciaEmpresa de tipo Ubigeo
        // Validación personalizada para DistritoEmpresa de tipo Ubigeo
        RuleFor(x => x.Direccion)
            .NotEmpty().WithMessage("El campo Direccion no puede estar vacío.");
        // Validación personalizada para Organizacion de tipo Organizacion
    }
}
