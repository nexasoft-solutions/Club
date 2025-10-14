using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Masters.Ubigeos.Request;
using NexaSoft.Club.Application.Masters.Ubigeos.Commands.CreateUbigeo;
using NexaSoft.Club.Application.Masters.Ubigeos.Commands.UpdateUbigeo;
using NexaSoft.Club.Application.Masters.Ubigeos.Commands.DeleteUbigeo;
using NexaSoft.Club.Application.Masters.Ubigeos.Queries.GetUbigeo;
using NexaSoft.Club.Application.Masters.Ubigeos.Queries.GetUbigeos;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using NexaSoft.Club.Api.Controllers.Masters.Ubigeos.Requests;

namespace NexaSoft.Club.Api.Controllers.Masters.Ubigeos;

[Route("api/[controller]")]
[ApiController]
public class UbigeoController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateUbigeo(CreateUbigeoRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateUbigeoCommand(
             request.Description,
             request.Level,
             request.ParentId,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUbigeo(UpdateUbigeoRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateUbigeoCommand(
           request.Id,
             request.Description,
             request.Level,
             request.ParentId,
             request.UserModification!
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUbigeo(DeleteUbigeoRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteUbigeoCommand(
             request.Id,
             request.UserDelete!
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetUbigeos(
       [FromQuery] BaseSpecParams<long> specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetUbigeosQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetUbigeo(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetUbigeoQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
