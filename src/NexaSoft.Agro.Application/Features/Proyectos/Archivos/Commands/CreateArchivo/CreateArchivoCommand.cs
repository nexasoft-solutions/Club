using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Archivos.Commands.CreateArchivo;

public sealed record CreateArchivoCommand(
    string? NombreArchivo,
    string? DescripcionArchivo,
    int TipoArchivoId,
    long? SubCapituloId,
    long? EstructuraId,
    string? NombreCorto,
    Stream ArchivoStream,
    string ArchivoTipo,
    string? UsuarioCreacion
) : ICommand<long>;
