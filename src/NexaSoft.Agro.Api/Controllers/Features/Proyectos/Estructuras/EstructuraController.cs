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
             request.SubCapituloId
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
             request.SubCapituloId
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteEstructura(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteEstructuraCommand(
             id
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


    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetEstructura(
        Guid id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetEstructuraQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }
}
