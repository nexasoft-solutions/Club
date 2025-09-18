using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Agro.Api.Controllers.Features.Proyectos.Responsables.Request;
using NexaSoft.Agro.Application.Features.Proyectos.Responsables.Commands.CreateResponsable;
using NexaSoft.Agro.Application.Features.Proyectos.Responsables.Commands.UpdateResponsable;
using NexaSoft.Agro.Application.Features.Proyectos.Responsables.Commands.DeleteResponsable;
using NexaSoft.Agro.Application.Features.Proyectos.Responsables.Queries.GetResponsable;
using NexaSoft.Agro.Application.Features.Proyectos.Responsables.Queries.GetResponsables;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Api.Extensions;
using NexaSoft.Agro.Api.Controllers.Features.Proyectos.Responsables.Requests;

namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Responsables;

[Route("api/[controller]")]
[ApiController]
public class ResponsableController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateResponsable(CreateResponsableRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateResponsableCommand(
             request.NombreResponsable,
             request.CargoResponsable,
             request.CorreoResponsable,
             request.TelefonoResponsable,
             request.Observaciones,
             request.EstudioAmbientalId,
             request.UsuarioCreacion
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateResponsable(UpdateResponsableRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateResponsableCommand(
             request.Id,
             request.NombreResponsable,
             request.CargoResponsable,
             request.CorreoResponsable,
             request.TelefonoResponsable,
             request.Observaciones,
             request.UsuarioModificacion
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteResponsable(DeleteResponsableRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteResponsableCommand(
             request.Id,
             request.UsuarioEliminacion
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetResponsables(
       [FromQuery] BaseSpecParams<long> specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetResponsablesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetResponsable(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetResponsableQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
