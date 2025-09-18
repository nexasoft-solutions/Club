using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Capitulos.Commands.DeleteCapitulo;

public sealed record DeleteCapituloCommand(
    long Id,
    string UsuarioEliminacion
) : ICommand<bool>;
