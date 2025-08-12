using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Agro.Api.Controllers.Features.Proyectos.SubCapitulos.Request;
using NexaSoft.Agro.Application.Features.Proyectos.SubCapitulos.Commands.CreateSubCapitulo;
using NexaSoft.Agro.Application.Features.Proyectos.SubCapitulos.Commands.UpdateSubCapitulo;
using NexaSoft.Agro.Application.Features.Proyectos.SubCapitulos.Commands.DeleteSubCapitulo;
using NexaSoft.Agro.Application.Features.Proyectos.SubCapitulos.Queries.GetSubCapitulo;
using NexaSoft.Agro.Application.Features.Proyectos.SubCapitulos.Queries.GetSubCapitulos;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Api.Extensions;

namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.SubCapitulos;

[Route("api/[controller]")]
[ApiController]
public class SubCapituloController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateSubCapitulo(CreateSubCapituloRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateSubCapituloCommand(
             request.NombreSubCapitulo,
             request.DescripcionSubCapitulo,
             request.CapituloId
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSubCapitulo(UpdateSubCapituloRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateSubCapituloCommand(
           request.Id,
             request.NombreSubCapitulo,
             request.DescripcionSubCapitulo,
             request.CapituloId
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteSubCapitulo(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteSubCapituloCommand(
             id
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetSubCapitulos(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetSubCapitulosQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetSubCapitulo(
        Guid id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetSubCapituloQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
