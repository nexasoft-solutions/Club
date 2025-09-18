using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Agro.Api.Controllers.Features.Proyectos.Estructuras.Request;
using NexaSoft.Agro.Application.Features.Proyectos.Estructuras.Commands.CreateEstructura;
using NexaSoft.Agro.Application.Features.Proyectos.Estructuras.Commands.UpdateEstructura;
using NexaSoft.Agro.Application.Features.Proyectos.Estructuras.Commands.DeleteEstructura;
using NexaSoft.Agro.Application.Features.Proyectos.Estructuras.Queries.GetEstructura;
using NexaSoft.Agro.Application.Features.Proyectos.Estructuras.Queries.GetEstructuras;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Api.Extensions;
using NexaSoft.Agro.Api.Controllers.Features.Proyectos.Estructuras.Requests;

namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Estructuras;

[Route("api/[controller]")]
[ApiController]
public class EstructuraController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateEstructura(CreateEstructuraRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateEstructuraCommand(
             request.TipoEstructuraId,
             request.NombreEstructura,
             request.DescripcionEstructura,
             request.PadreEstructuraId,
             request.SubCapituloId,
             request.UsuarioCreacion
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateEstructura(UpdateEstructuraRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateEstructuraCommand(
           request.Id,
             request.TipoEstructuraId,
             request.NombreEstructura,
             request.DescripcionEstructura,
             request.PadreEstructuraId,
             request.SubCapituloId,
             request.UsuarioModificacion
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEstructura(DeleteEstructuraRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteEstructuraCommand(
             request.Id,
             request.UsuarioEliminacion
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetEstructuras(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetEstructurasQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetEstructura(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetEstructuraQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }
}
