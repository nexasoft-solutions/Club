using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.LegalParameters.Request;
using NexaSoft.Club.Application.HumanResources.LegalParameters.Commands.CreateLegalParameter;
using NexaSoft.Club.Application.HumanResources.LegalParameters.Commands.UpdateLegalParameter;
using NexaSoft.Club.Application.HumanResources.LegalParameters.Commands.DeleteLegalParameter;
using NexaSoft.Club.Application.HumanResources.LegalParameters.Queries.GetLegalParameter;
using NexaSoft.Club.Application.HumanResources.LegalParameters.Queries.GetLegalParameters;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.LegalParameters;

[Route("api/[controller]")]
[ApiController]
public class LegalParameterController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateLegalParameter(CreateLegalParameterRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateLegalParameterCommand(
             request.Code,
             request.Name,
             request.Value,
             request.ValueText,
             request.EffectiveDate,
             request.EndDate,
             request.Category,
             request.Description,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateLegalParameter(UpdateLegalParameterRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateLegalParameterCommand(
           request.Id,
             request.Code,
             request.Name,
             request.Value,
             request.ValueText,
             request.EffectiveDate,
             request.EndDate,
             request.Category,
             request.Description,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeleteLegalParameter(DeleteLegalParameterRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteLegalParameterCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetLegalParameters(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetLegalParametersQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetLegalParameter(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetLegalParameterQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
