using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.ConceptTypePayrolls.Request;
using NexaSoft.Club.Application.HumanResources.ConceptTypePayrolls.Commands.CreateConceptTypePayroll;
using NexaSoft.Club.Application.HumanResources.ConceptTypePayrolls.Commands.UpdateConceptTypePayroll;
using NexaSoft.Club.Application.HumanResources.ConceptTypePayrolls.Commands.DeleteConceptTypePayroll;
using NexaSoft.Club.Application.HumanResources.ConceptTypePayrolls.Queries.GetConceptTypePayroll;
using NexaSoft.Club.Application.HumanResources.ConceptTypePayrolls.Queries.GetConceptTypePayrolls;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.ConceptTypePayrolls;

[Route("api/[controller]")]
[ApiController]
public class ConceptTypePayrollController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateConceptTypePayroll(CreateConceptTypePayrollRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateConceptTypePayrollCommand(
             request.Code,
             request.Name,
             request.Description,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateConceptTypePayroll(UpdateConceptTypePayrollRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateConceptTypePayrollCommand(
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
   public async Task<IActionResult> DeleteConceptTypePayroll(DeleteConceptTypePayrollRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteConceptTypePayrollCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetConceptTypePayrolls(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetConceptTypePayrollsQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetConceptTypePayroll(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetConceptTypePayrollQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
