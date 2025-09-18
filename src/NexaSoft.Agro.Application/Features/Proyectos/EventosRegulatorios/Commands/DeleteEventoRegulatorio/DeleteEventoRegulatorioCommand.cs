using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Commands.DeleteEventoRegulatorio;

public sealed record DeleteEventoRegulatorioCommand(
    long Id,
    string UsuarioEliminacion
) : ICommand<bool>;
