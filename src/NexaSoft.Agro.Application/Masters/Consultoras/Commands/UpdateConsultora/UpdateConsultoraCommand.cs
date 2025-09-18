using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Consultoras.Commands.UpdateConsultora;

public sealed record UpdateConsultoraCommand(
    long Id,
    string? NombreConsultora,
    string? DireccionConsultora,
    string? RepresentanteConsultora,
    string? RucConsultora,
    string? CorreoOrganizacional,
    string? UsuarioModificacion
) : ICommand<bool>;
