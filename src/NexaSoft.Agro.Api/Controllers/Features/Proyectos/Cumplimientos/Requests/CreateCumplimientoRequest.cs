namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Cumplimientos.Request;

public sealed record CreateCumplimientoRequest(
    DateOnly? FechaCumplimiento,
    bool? RegistradoaTiempo,
    string? Observaciones,
    long EventoRegulatorioId,
    string? UsuarioCreacion
);
