using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Capitulos.Commands.CreateCapitulo;

public sealed record CreateCapituloCommand(
    string? NombreCapitulo,
    string? DescripcionCapitulo,
    long EstudioAmbientalId,
    string? UsuarioCreacion
) : ICommand<long>;
