using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Archivos.Commands.UpdateArchivo;

public sealed record UpdateArchivoCommand(
    long Id,
    string? DescripcionArchivo,
    string? NombreCorto,
    string? UsuarioModificacion
) : ICommand<bool>;
