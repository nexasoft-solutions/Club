namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.EventosRegulatorios.Requests;

public sealed record  AddResponsablesEventoRequest
(
    long EventoRegulatorioId,
    List<long> ResponsablesIds,
    string UsuarioCreacion
);
