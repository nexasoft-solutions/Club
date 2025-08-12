using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Estructuras.Commands.UpdateEstructura;

public sealed record UpdateEstructuraCommand(
    Guid Id,
    int TipoEstructuraId,
    string? NombreEstructura,
    string? DescripcionEstructura,
    Guid? PadreEstructuraId,
    Guid SubCapituloId
) : ICommand<bool>;
