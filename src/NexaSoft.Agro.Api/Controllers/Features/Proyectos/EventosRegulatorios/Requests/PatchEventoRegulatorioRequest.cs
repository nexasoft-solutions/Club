namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.EventosRegulatorios.Requests;

public sealed record PatchEventoRegulatorioRequest
(
    long Id,
    int NuevoEstado,
    string Observaciones,
    string UsuarioModificacion,
    DateOnly? FechaVencimiento = null
);
