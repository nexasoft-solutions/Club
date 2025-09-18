using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Commands.DeleteOrganizacion;

public sealed record DeleteOrganizacionCommand(
    long Id,
    String UsuarioEliminacion
) : ICommand<bool>;
