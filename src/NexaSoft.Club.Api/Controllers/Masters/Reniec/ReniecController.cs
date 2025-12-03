using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Attributes;
using NexaSoft.Club.Application.Masters.Reniec.Queries;
using NexaSoft.Club.Domain.ServicesModel.Reniec;

namespace NexaSoft.Club.Api.Controllers.Masters.Reniec;

[ApiController]
[Route("api/masters/reniec")]
[Authorize]
public class ReniecController(ISender _sender) : ControllerBase
{
    /*private readonly IMediator _mediator;

    public ReniecController(IMediator mediator)
    {
        _mediator = mediator;
    }*/

    [HttpGet("dni/{dni}")]
    [GeneratePermission]
    [RequirePermission("Reniec.GetReniec")]
    public async Task<IActionResult> GetDni(string dni, CancellationToken cancellationToken)
    {
        var query = new GetReniecDniQuery(dni);
        var resultado = await _sender.Send(query, cancellationToken);
        if (resultado is null) return NotFound();
        return Ok(resultado);
    }

    [HttpGet("ruc/{ruc}")]
    [GeneratePermission]
    [RequirePermission("Reniec.GetReniec")]
    public async Task<IActionResult> GetRuc(string ruc, CancellationToken cancellationToken)
    {
        var query = new GetReniecRucQuery(ruc);
        var resultado = await _sender.Send(query, cancellationToken);
        if (resultado is null) return NotFound();
        return Ok(resultado);
    }
}
