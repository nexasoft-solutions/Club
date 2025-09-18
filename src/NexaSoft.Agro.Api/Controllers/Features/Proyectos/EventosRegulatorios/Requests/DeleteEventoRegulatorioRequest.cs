namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.EventosRegulatorios.Requests;

public record class DeleteEventoRegulatorioRequest
(
    long Id,
    string UsuarioEliminacion
);