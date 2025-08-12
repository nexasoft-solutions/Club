using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Archivos.Commands.CreateArchivo;

public sealed record CreateArchivoCommand(
    string? NombreArchivo,
    string? DescripcionArchivo,
    int TipoArchivo,
    Guid? SubCapituloId,
    Guid? EstructuraId,
    Stream ArchivoStream,
    string ArchivoTipo
) : ICommand<Guid>;
