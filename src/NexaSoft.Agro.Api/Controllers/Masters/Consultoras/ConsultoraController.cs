using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Agro.Api.Controllers.Masters.Consultoras.Request;
using NexaSoft.Agro.Application.Masters.Consultoras.Commands.CreateConsultora;
using NexaSoft.Agro.Application.Masters.Consultoras.Commands.UpdateConsultora;
using NexaSoft.Agro.Application.Masters.Consultoras.Commands.DeleteConsultora;
using NexaSoft.Agro.Application.Masters.Consultoras.Queries.GetConsultora;
using NexaSoft.Agro.Application.Masters.Consultoras.Queries.GetConsultoras;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Api.Extensions;

namespace NexaSoft.Agro.Api.Controllers.Masters.Consultoras;

[Route("api/[controller]")]
[ApiController]
public class ConsultoraController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateConsultora(CreateConsultoraRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateConsultoraCommand(
             request.NombreConsultora,
             request.DireccionConsultora,
             request.RepresentanteConsultora,
             request.RucConsultora,
             request.CorreoOrganizacional
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateConsultora(UpdateConsultoraRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateConsultoraCommand(
           request.Id,
             request.NombreConsultora,
             request.DireccionConsultora,
             request.RepresentanteConsultora,
             request.RucConsultora,
             request.CorreoOrganizacional
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteConsultora(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteConsultoraCommand(
             id
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetConsultoras(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetConsultorasQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetConsultora(
        Guid id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetConsultoraQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
