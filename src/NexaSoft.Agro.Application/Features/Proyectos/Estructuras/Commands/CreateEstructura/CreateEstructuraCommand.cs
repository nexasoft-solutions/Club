using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Estructuras.Commands.CreateEstructura;

public sealed record CreateEstructuraCommand(
    int TipoEstructuraId,
    string? NombreEstructura,
    string? DescripcionEstructura,
    Guid? PadreEstructuraId,
    Guid SubCapituloId
) : ICommand<Guid>;
