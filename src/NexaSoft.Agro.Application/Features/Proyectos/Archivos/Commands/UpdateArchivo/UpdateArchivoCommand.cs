using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Archivos.Commands.UpdateArchivo;

public sealed record UpdateArchivoCommand(
    Guid Id,
    string? DescripcionArchivo
) : ICommand<bool>;
