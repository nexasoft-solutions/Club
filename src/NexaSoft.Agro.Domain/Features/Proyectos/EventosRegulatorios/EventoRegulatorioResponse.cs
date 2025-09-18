using NexaSoft.Agro.Domain.Features.Proyectos.Responsables;

namespace NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios;

public sealed record EventoRegulatorioResponse(
    long Id,
    string? NombreEvento,
    string? TipoEvento,
    string? FrecuenciaEvento,
    DateOnly? FechaExpedición,
    DateOnly? FechaVencimiento,
    string? Descripcion,
    int NotificarDíasAntes,
    long ResponsableId,
    string? Responsable,
    string? CorreoResponsable,
    string? TelefonoResponsable,
    string? EstadoEvento,
    long EstudioAmbientalId,
    int TipoEventoId,
    int FrecuenciaEventoId,
    int EstadoEventoId,
    string? Proyecto,
    DateTime FechaCreacion,
    DateTime? FechaModificacion,
    string? UsuarioCreacion,
    string? UsuarioModificacion,
    List<EventoRegulatorioResponsable>? Responsables = null
);
