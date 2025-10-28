using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.ConceptApplicationTypes.Request;
using NexaSoft.Club.Application.HumanResources.ConceptApplicationTypes.Commands.CreateConceptApplicationType;
using NexaSoft.Club.Application.HumanResources.ConceptApplicationTypes.Commands.UpdateConceptApplicationType;
using NexaSoft.Club.Application.HumanResources.ConceptApplicationTypes.Commands.DeleteConceptApplicationType;
using NexaSoft.Club.Application.HumanResources.ConceptApplicationTypes.Queries.GetConceptApplicationType;
using NexaSoft.Club.Application.HumanResources.ConceptApplicationTypes.Queries.GetConceptApplicationTypes;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.ConceptApplicationTypes;

[Route("api/[controller]")]
[ApiController]
public class ConceptApplicationTypeController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateConceptApplicationType(CreateConceptApplicationTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateConceptApplicationTypeCommand(
             request.Code,
             request.Name,
             request.Description,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateConceptApplicationType(UpdateConceptApplicationTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateConceptApplicationTypeCommand(
           request.Id,
             request.Code,
             request.Name,
             request.Description,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeleteConceptApplicationType(DeleteConceptApplicationTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteConceptApplicationTypeCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetConceptApplicationTypes(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetConceptApplicationTypesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetConceptApplicationType(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetConceptApplicationTypeQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
