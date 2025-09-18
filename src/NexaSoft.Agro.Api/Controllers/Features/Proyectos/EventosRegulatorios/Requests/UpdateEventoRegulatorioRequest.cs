namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.EventosRegulatorios.Request;

public sealed record UpdateEventoRegulatorioRequest(
   long Id,
    string? NombreEvento,
    int TipoEventoId,
    int FrecuenciaEventoId,
    DateOnly? FechaExpedición,
    DateOnly? FechaVencimiento,
    string? Descripcion,
    int NotificarDíasAntes,
    long ResponsableId,
    long EstudioAmbientalId,
    string? UsuarioModificacion,
    IEnumerable<long>? ResponsablesAdicionales = null
);
