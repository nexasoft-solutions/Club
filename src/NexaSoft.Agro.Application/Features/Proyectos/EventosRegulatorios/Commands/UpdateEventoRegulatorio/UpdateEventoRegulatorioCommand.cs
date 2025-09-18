using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Commands.UpdateEventoRegulatorio;

public sealed record UpdateEventoRegulatorioCommand(
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
) : ICommand<bool>;
