using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Cumplimientos.Commands.CreateCumplimiento;

public sealed record CreateCumplimientoCommand(
    DateOnly? FechaCumplimiento,
    bool? RegistradoaTiempo,
    string? Observaciones,
    long EventoRegulatorioId,
    string? UsuarioCreacion
) : ICommand<long>;
