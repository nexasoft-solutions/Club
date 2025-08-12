using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Capitulos.Commands.UpdateCapitulo;

public sealed record UpdateCapituloCommand(
    Guid Id,
    string? NombreCapitulo,
    string? DescripcionCapitulo,
    Guid EstudioAmbientalId
) : ICommand<bool>;
