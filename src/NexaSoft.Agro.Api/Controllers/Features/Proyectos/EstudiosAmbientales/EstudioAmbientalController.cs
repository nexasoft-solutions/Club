using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Agro.Api.Controllers.Features.Proyectos.EstudiosAmbientales.Request;
using NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Commands.CreateEstudioAmbiental;
using NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Commands.UpdateEstudioAmbiental;
using NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Commands.DeleteEstudioAmbiental;
using NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Queries.GetEstudioAmbiental;
using NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Queries.GetEstudiosAmbientales;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Api.Extensions;
using NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Queries.GetEstudioAmbientalById;
using NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Queries.GetEstudioAmbientalCapitulosById;
using NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Queries.GetEstudioEstructurasById;

namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.EstudiosAmbientales;

[Route("api/[controller]")]
[ApiController]
public class EstudioAmbientalController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateEstudioAmbiental(CreateEstudioAmbientalRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateEstudioAmbientalCommand(
             request.Proyecto,
             request.FechaInicio,
             request.FechaFin,
             request.Detalles,
             request.EmpresaId
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateEstudioAmbiental(UpdateEstudioAmbientalRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateEstudioAmbientalCommand(
             request.Id,
             request.Proyecto,
             request.FechaInicio,
             request.FechaFin,
             request.Detalles,
             request.EmpresaId
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteEstudioAmbiental(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteEstudioAmbientalCommand(
             id
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetEstudiosAmbientales(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetEstudiosAmbientalesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetEstudioAmbiental(
        Guid id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetEstudioAmbientalQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet("detalle/{id:Guid}")]
    public async Task<IActionResult> GetEstudioAmbientalById(
      Guid id,
      CancellationToken cancellationToken
   )
    {
        var query = new GetEstudioAmbientalByIdQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet("estudio-ambiental-capitulos/{id:Guid}")]
    public async Task<IActionResult> GetEstudioAmbientalCapitulosById(
     Guid id,
     CancellationToken cancellationToken
    )
    {
        var query = new GetEstudioAmbientalCapitulosByIdQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

     [HttpGet("estudio-ambiental-estructuras/{id:Guid}")]
    public async Task<IActionResult> GetEstudioAmbientalEstrucuturasById(
     Guid id,
     CancellationToken cancellationToken
    )
    {
        var query = new GetEstudioEstructurasByIdQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
