using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Cumplimientos.Commands.DeleteCumplimiento;

public sealed record DeleteCumplimientoCommand(
    long Id,
    string UsuarioEliminacion
) : ICommand<bool>;
