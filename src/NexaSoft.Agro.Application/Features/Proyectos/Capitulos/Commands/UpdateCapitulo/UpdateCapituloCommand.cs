using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Capitulos.Commands.UpdateCapitulo;

public sealed record UpdateCapituloCommand(
    long Id,
    string? NombreCapitulo,
    string? DescripcionCapitulo,
    long EstudioAmbientalId,
    string? UsuarioModificacion
) : ICommand<bool>;
