using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Archivos.Commands.DeleteArchivo;

public sealed record DeleteArchivoCommand(
    Guid Id
) : ICommand<bool>;
