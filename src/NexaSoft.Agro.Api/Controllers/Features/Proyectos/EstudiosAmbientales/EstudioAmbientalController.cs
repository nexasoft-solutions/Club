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
using NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Queries.GetEstudiosReport;
using NexaSoft.Agro.Api.Controllers.Features.Proyectos.EstudiosAmbientales.Requests;

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
             request.EmpresaId,
             request.UsuarioCreacion
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
             request.EmpresaId,
             request.UsuarioModificacion
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEstudioAmbiental(DeleteEstudioAmbientalRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteEstudioAmbientalCommand(
             request.Id,
             request.UsuarioEliminacion
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


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetEstudioAmbiental(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetEstudioAmbientalQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet("detalle/{id:long}")]
    public async Task<IActionResult> GetEstudioAmbientalById(
      long id,
      CancellationToken cancellationToken
   )
    {
        var query = new GetEstudioAmbientalByIdQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet("estudio-ambiental-capitulos/{id:long}")]
    public async Task<IActionResult> GetEstudioAmbientalCapitulosById(
     long id,
     CancellationToken cancellationToken
    )
    {
        var query = new GetEstudioAmbientalCapitulosByIdQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet("estudio-ambiental-estructuras/{id:long}")]
    public async Task<IActionResult> GetEstudioAmbientalEstrucuturasById(
    long id,
    CancellationToken cancellationToken
   )
    {
        var query = new GetEstudioEstructurasByIdQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet("reporte")]
    public async Task<IActionResult> GetEstudiosAmbientalesReport(
       [FromQuery] long Id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetEstudioReportQuery(Id);
        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
            return result.ToActionResult(this);

        // Retornar el PDF como archivo descargable
        return File(
            fileContents: result.Value,
            contentType: "application/pdf",
            fileDownloadName: "Reporte_Estudio.pdf"
        );
    }

}
