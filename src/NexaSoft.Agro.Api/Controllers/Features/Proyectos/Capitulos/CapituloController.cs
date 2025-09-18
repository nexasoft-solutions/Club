using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Agro.Api.Controllers.Features.Proyectos.Capitulos.Request;
using NexaSoft.Agro.Application.Features.Proyectos.Capitulos.Commands.CreateCapitulo;
using NexaSoft.Agro.Application.Features.Proyectos.Capitulos.Commands.UpdateCapitulo;
using NexaSoft.Agro.Application.Features.Proyectos.Capitulos.Commands.DeleteCapitulo;
using NexaSoft.Agro.Application.Features.Proyectos.Capitulos.Queries.GetCapitulo;
using NexaSoft.Agro.Application.Features.Proyectos.Capitulos.Queries.GetCapitulos;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Api.Extensions;
using NexaSoft.Agro.Api.Controllers.Features.Proyectos.Capitulos.Requests;

namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Capitulos;

[Route("api/[controller]")]
[ApiController]
public class CapituloController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateCapitulo(CreateCapituloRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCapituloCommand(
             request.NombreCapitulo,
             request.DescripcionCapitulo,
             request.EstudioAmbientalId,
             request.UsuarioCreacion
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCapitulo(UpdateCapituloRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateCapituloCommand(
           request.Id,
             request.NombreCapitulo,
             request.DescripcionCapitulo,
             request.EstudioAmbientalId,
             request.UsuarioModificacion
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCapitulo(DeleteCapituloRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteCapituloCommand(
             request.Id,
             request.UsuarioEliminacion
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetCapitulos(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetCapitulosQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetCapitulo(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetCapituloQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
