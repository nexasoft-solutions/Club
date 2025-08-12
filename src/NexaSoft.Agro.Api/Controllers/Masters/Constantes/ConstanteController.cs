using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Agro.Api.Controllers.Masters.Constantes.Request;
using NexaSoft.Agro.Application.Masters.Constantes.Commands.CreateConstante;
using NexaSoft.Agro.Application.Masters.Constantes.Commands.UpdateConstante;
using NexaSoft.Agro.Application.Masters.Constantes.Commands.DeleteConstante;
using NexaSoft.Agro.Application.Masters.Constantes.Queries.GetConstante;
using NexaSoft.Agro.Application.Masters.Constantes.Queries.GetConstantes;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Api.Extensions;
using NexaSoft.Agro.Application.Masters.Constantes.Queries.GetConstantesTipo;
using NexaSoft.Agro.Application.Masters.Constantes.Queries.GetConstesMultiple;
using NexaSoft.Agro.Api.Attributes;

namespace NexaSoft.Agro.Api.Controllers.Masters.Constantes;

[Route("api/[controller]")]
[ApiController]
//[RequireRole("Administrador")]
public class ConstanteController(ISender _sender) : ControllerBase
{

    [HttpPost]
    [RequireRole("Administrador")]
    //[RequirePermission("Constante.CreateConstante")]
    public async Task<IActionResult> CreateConstante(CreateConstanteRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateConstanteCommand(
             request.TipoConstante,
             request.Valor
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    [RequireRole("Administrador")] // O solo [RequirePermission("Constante.DeleteConstante")]
    [RequirePermission("Constante.UpdateConstante")]
    public async Task<IActionResult> UpdateConstante(UpdateConstanteRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateConstanteCommand(
             request.Id,
             request.TipoConstante,
             request.Valor
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteConstante(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteConstanteCommand(
             id
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    [RequireRole("Administrador")]
    [RequirePermission("Constante.GetConstantes")]
    public async Task<IActionResult> GetConstantes(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetConstantesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetConstante(
        Guid id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetConstanteQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet("tipo")]
    public async Task<IActionResult> GetConstantesTipo(
       CancellationToken cancellationToken
    )
    {
        var query = new GetConstantesTipoQuery();
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }
    
    [HttpPost("multiple")]
    public async Task<IActionResult> GetConstantesMultiples(
        [FromBody] List<string> tiposConstante,
        CancellationToken cancellationToken)
    {
        var query = new GetConstnatesMultipleQuery(tiposConstante);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
