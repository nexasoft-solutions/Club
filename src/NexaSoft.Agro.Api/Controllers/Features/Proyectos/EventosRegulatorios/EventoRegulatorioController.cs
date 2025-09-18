using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Agro.Api.Controllers.Features.Proyectos.EventosRegulatorios.Request;
using NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Commands.CreateEventoRegulatorio;
using NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Commands.UpdateEventoRegulatorio;
using NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Commands.DeleteEventoRegulatorio;
using NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Queries.GetEventoRegulatorio;
using NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Queries.GetEventosRegulatorios;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Api.Extensions;
using NexaSoft.Agro.Api.Controllers.Features.Proyectos.EventosRegulatorios.Requests;
using NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Commands.AddResposablesEvento;
using NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Commands.PatchEventoRegulatorio;

namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.EventosRegulatorios;

[Route("api/[controller]")]
[ApiController]
public class EventoRegulatorioController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateEventoRegulatorio(CreateEventoRegulatorioRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateEventoRegulatorioCommand(
             request.NombreEvento,
             request.TipoEventoId,
             request.FrecuenciaEventoId,
             request.FechaExpedición,
             request.FechaVencimiento,
             request.Descripcion,
             request.NotificarDíasAntes,
             request.ResponsableId,
             request.EstudioAmbientalId,
             request.UsuarioCreacion,
             request.ResponsablesAdicionales
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateEventoRegulatorio(UpdateEventoRegulatorioRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateEventoRegulatorioCommand(
             request.Id,
             request.NombreEvento,
             request.TipoEventoId,
             request.FrecuenciaEventoId,
             request.FechaExpedición,
             request.FechaVencimiento,
             request.Descripcion,
             request.NotificarDíasAntes,
             request.ResponsableId,
             request.EstudioAmbientalId,
             request.UsuarioModificacion,
             request.ResponsablesAdicionales
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPatch]
    public async Task<IActionResult> PatchEventoRegulatorio(PatchEventoRegulatorioRequest request, CancellationToken cancellationToken)
    {
        var command = new PatchEventoRegulatorioCommand(
             request.Id,
             request.NuevoEstado,
             request.Observaciones,          
             request.UsuarioModificacion,
             request.FechaVencimiento
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPost("responsables")]
    public async Task<IActionResult> AddResponsablesEvento(
    [FromBody] AddResponsablesEventoRequest request,
    CancellationToken cancellationToken)
    {
        var command = new AddResponsablesEventoCommand(
            request.EventoRegulatorioId,
            request.ResponsablesIds,
            request.UsuarioCreacion
        );

        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEventoRegulatorio(DeleteEventoRegulatorioRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteEventoRegulatorioCommand(
             request.Id,
             request.UsuarioEliminacion
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetEventosRegulatorios(
       [FromQuery] BaseSpecParams<long> specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetEventosRegulatoriosQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetEventoRegulatorio(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetEventoRegulatorioQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
