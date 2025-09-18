namespace NexaSoft.Agro.Domain.Features.Proyectos.Cumplimientos;

public sealed record CumplimientoResponse(
    long Id,
    DateOnly? FechaCumplimiento,
    bool? RegistradoaTiempo,
    string? Observaciones,
    long EventoRegulatorioId,
    string? NombreEvento,
    DateTime FechaCreacion,
    DateTime? FechaModificacion,
    string? UsuarioCreacion,
    string? UsuarioModificacion
);
