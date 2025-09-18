using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Commands.DeleteEstudioAmbiental;

public sealed record DeleteEstudioAmbientalCommand(
    long Id,
    string UsuarioEliminacion
) : ICommand<bool>;
