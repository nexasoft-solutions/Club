using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Empresas.Commands.DeleteEmpresa;

public sealed record DeleteEmpresaCommand(
    long Id,
    string UsuarioEliminacion
) : ICommand<bool>;
