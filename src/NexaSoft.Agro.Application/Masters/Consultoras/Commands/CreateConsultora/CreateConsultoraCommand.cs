using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Consultoras.Commands.CreateConsultora;

public sealed record CreateConsultoraCommand(
    string? NombreConsultora,
    string? DireccionConsultora,
    string? RepresentanteConsultora,
    string? RucConsultora,
    string? CorreoOrganizacional,
    string? UsuarioCreacion
) : ICommand<long>;
