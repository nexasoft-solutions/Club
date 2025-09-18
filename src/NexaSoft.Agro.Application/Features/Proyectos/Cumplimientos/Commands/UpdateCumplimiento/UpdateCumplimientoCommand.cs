using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Cumplimientos.Commands.UpdateCumplimiento;

public sealed record UpdateCumplimientoCommand(
    long Id,
    DateOnly? FechaCumplimiento,
    bool? RegistradoaTiempo,
    string? Observaciones,
    long EventoRegulatorioId,
    string? UsuarioModificacion
) : ICommand<bool>;
