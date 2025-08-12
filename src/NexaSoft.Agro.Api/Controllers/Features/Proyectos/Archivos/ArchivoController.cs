using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Agro.Api.Controllers.Features.Proyectos.Archivos.Request;
using NexaSoft.Agro.Application.Features.Proyectos.Archivos.Commands.CreateArchivo;
using NexaSoft.Agro.Application.Features.Proyectos.Archivos.Commands.UpdateArchivo;
using NexaSoft.Agro.Application.Features.Proyectos.Archivos.Commands.DeleteArchivo;
using NexaSoft.Agro.Application.Features.Proyectos.Archivos.Queries.GetArchivo;
using NexaSoft.Agro.Application.Features.Proyectos.Archivos.Queries.GetArchivos;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Api.Extensions;
using NexaSoft.Agro.Application.Features.Proyectos.Archivos.Queries.GetArchivoDownload;

namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Archivos;

[Route("api/[controller]")]
[ApiController]
public class ArchivoController(ISender _sender) : ControllerBase
{

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CreateArchivo([FromForm] CreateArchivoRequest request, CancellationToken cancellationToken)
    {
        using var stream = request.Archivo.OpenReadStream();

        var command = new CreateArchivoCommand(
             request.Archivo.FileName,
             request.DescripcionArchivo,
             request.TipoArchivo,
             request.SubCapituloId,
             request.EstructuraId,
             stream,
             request.Archivo.ContentType
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateArchivo(UpdateArchivoRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateArchivoCommand(
             request.Id,
             request.DescripcionArchivo
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteArchivo(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteArchivoCommand(
             id
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetArchivos(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetArchivosQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetArchivo(
        Guid id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetArchivoQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet("{id}/descargar")]
    public async Task<IActionResult> DescargarArchivo(Guid id, CancellationToken cancellationToken)
    {

        var query = new GetArchivoDownloadQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
