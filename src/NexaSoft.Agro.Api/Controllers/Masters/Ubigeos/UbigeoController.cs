using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Agro.Api.Controllers.Masters.Ubigeos.Request;
using NexaSoft.Agro.Application.Masters.Ubigeos.Commands.CreateUbigeo;
using NexaSoft.Agro.Application.Masters.Ubigeos.Commands.UpdateUbigeo;
using NexaSoft.Agro.Application.Masters.Ubigeos.Commands.DeleteUbigeo;
using NexaSoft.Agro.Application.Masters.Ubigeos.Queries.GetUbigeo;
using NexaSoft.Agro.Application.Masters.Ubigeos.Queries.GetUbigeos;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Api.Extensions;

namespace NexaSoft.Agro.Api.Controllers.Masters.Ubigeos;

[Route("api/[controller]")]
[ApiController]
public class UbigeoController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateUbigeo(CreateUbigeoRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateUbigeoCommand(
             request.Descripcion,
             request.Nivel,
             request.PadreId
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUbigeo(UpdateUbigeoRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateUbigeoCommand(
           request.Id,
             request.Descripcion,
             request.Nivel,
             request.PadreId
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteUbigeo(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteUbigeoCommand(
             id
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetUbigeos(
       [FromQuery] BaseSpecParams<Guid> specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetUbigeosQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetUbigeo(
        Guid id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetUbigeoQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
