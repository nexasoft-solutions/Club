using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.PayrollConcepts.Request;
using NexaSoft.Club.Application.HumanResources.PayrollConcepts.Commands.CreatePayrollConcept;
using NexaSoft.Club.Application.HumanResources.PayrollConcepts.Commands.UpdatePayrollConcept;
using NexaSoft.Club.Application.HumanResources.PayrollConcepts.Commands.DeletePayrollConcept;
using NexaSoft.Club.Application.HumanResources.PayrollConcepts.Queries.GetPayrollConcept;
using NexaSoft.Club.Application.HumanResources.PayrollConcepts.Queries.GetPayrollConcepts;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using NexaSoft.Club.Api.Controllers.HumanResources.PayrollConcepts.Requests;
using NexaSoft.Club.Application.HumanResources.PayrollConcepts.Commands.CreatePayrollConceptsForType;

namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollConcepts;

[Route("api/[controller]")]
[ApiController]
public class PayrollConceptController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreatePayrollConcept(CreatePayrollConceptRequest request, CancellationToken cancellationToken)
    {
        var command = new CreatePayrollConceptCommand(
             request.Code,
             request.Name,
             request.ConceptTypePayrollId,
             request.PayrollFormulaId,
             request.ConceptCalculationTypeId,
             request.FixedValue,
             request.PorcentajeValue,
             request.ConceptApplicationTypesId,
             request.AccountingChartId,
             //request.PayrollTypeId,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdatePayrollConcept(UpdatePayrollConceptRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdatePayrollConceptCommand(
             request.Id,
             request.Code,
             request.Name,
             request.ConceptTypePayrollId,
             request.PayrollFormulaId,
             request.ConceptCalculationTypeId,
             request.FixedValue,
             request.PorcentajeValue,
             request.ConceptApplicationTypesId,
             request.AccountingChartId,
             //request.PayrollTypeId,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeletePayrollConcept(DeletePayrollConceptRequest request, CancellationToken cancellationToken)
    {
        var command = new DeletePayrollConceptCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetPayrollConcepts(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPayrollConceptsQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetPayrollConcept(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetPayrollConceptQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }
    
    [HttpPost("CreateForType")] 
    public async Task<IActionResult> CreatePayrollConceptsForType(CreatePayrollConceptsForTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new CreatePayrollConceptsForTypeCommand(
             request.PayrollTypeId,
             request.PayrollConceptIds,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }
}
