using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Commands.CreateEventoRegulatorio;

public sealed record CreateEventoRegulatorioCommand(
    string? NombreEvento,
    int TipoEventoId,
    int FrecuenciaEventoId,
    DateOnly? FechaExpedición,
    DateOnly? FechaVencimiento,
    string? Descripcion,
    int NotificarDíasAntes,
    long ResponsableId,
    long EstudioAmbientalId,
    string? UsuarioCreacion,
    IEnumerable<long>? ResponsablesAdicionales = null
) : ICommand<long>;
