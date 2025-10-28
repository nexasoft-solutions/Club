using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.ConceptCalculationTypes.Request;
using NexaSoft.Club.Application.HumanResources.ConceptCalculationTypes.Commands.CreateConceptCalculationType;
using NexaSoft.Club.Application.HumanResources.ConceptCalculationTypes.Commands.UpdateConceptCalculationType;
using NexaSoft.Club.Application.HumanResources.ConceptCalculationTypes.Commands.DeleteConceptCalculationType;
using NexaSoft.Club.Application.HumanResources.ConceptCalculationTypes.Queries.GetConceptCalculationType;
using NexaSoft.Club.Application.HumanResources.ConceptCalculationTypes.Queries.GetConceptCalculationTypes;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.ConceptCalculationTypes;

[Route("api/[controller]")]
[ApiController]
public class ConceptCalculationTypeController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateConceptCalculationType(CreateConceptCalculationTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateConceptCalculationTypeCommand(
             request.Code,
             request.Name,
             request.Description,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateConceptCalculationType(UpdateConceptCalculationTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateConceptCalculationTypeCommand(
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
   public async Task<IActionResult> DeleteConceptCalculationType(DeleteConceptCalculationTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteConceptCalculationTypeCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetConceptCalculationTypes(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetConceptCalculationTypesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetConceptCalculationType(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetConceptCalculationTypeQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
