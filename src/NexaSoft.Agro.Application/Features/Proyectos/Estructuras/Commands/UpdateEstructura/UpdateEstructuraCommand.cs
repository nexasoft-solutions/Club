using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Estructuras.Commands.UpdateEstructura;

public sealed record UpdateEstructuraCommand(
    long Id,
    int TipoEstructuraId,
    string? NombreEstructura,
    string? DescripcionEstructura,
    long? PadreEstructuraId,
    long SubCapituloId,
    string? UsuarioModificacion
) : ICommand<bool>;
