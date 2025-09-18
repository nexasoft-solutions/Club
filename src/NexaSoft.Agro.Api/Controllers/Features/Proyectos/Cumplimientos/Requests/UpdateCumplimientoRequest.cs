namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Cumplimientos.Request;

public sealed record UpdateCumplimientoRequest(
   long Id,
    DateOnly? FechaCumplimiento,
    bool? RegistradoaTiempo,
    string? Observaciones,
    long EventoRegulatorioId,
    string? UsuarioModificacion
);
