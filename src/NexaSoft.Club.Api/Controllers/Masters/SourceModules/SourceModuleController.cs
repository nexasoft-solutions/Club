using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Masters.SourceModules.Request;
using NexaSoft.Club.Application.Masters.SourceModules.Commands.CreateSourceModule;
using NexaSoft.Club.Application.Masters.SourceModules.Commands.UpdateSourceModule;
using NexaSoft.Club.Application.Masters.SourceModules.Commands.DeleteSourceModule;
using NexaSoft.Club.Application.Masters.SourceModules.Queries.GetSourceModule;
using NexaSoft.Club.Application.Masters.SourceModules.Queries.GetSourceModules;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.Masters.SourceModules;

[Route("api/[controller]")]
[ApiController]
public class SourceModuleController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateSourceModule(CreateSourceModuleRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateSourceModuleCommand(
             request.Name,
             request.Description,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateSourceModule(UpdateSourceModuleRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateSourceModuleCommand(
           request.Id,
             request.Name,
             request.Description,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeleteSourceModule(DeleteSourceModuleRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteSourceModuleCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetSourceModules(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetSourceModulesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetSourceModule(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetSourceModuleQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
