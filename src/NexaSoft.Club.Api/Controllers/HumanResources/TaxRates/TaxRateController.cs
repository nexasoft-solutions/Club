using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.TaxRates.Request;
using NexaSoft.Club.Application.HumanResources.TaxRates.Commands.CreateTaxRate;
using NexaSoft.Club.Application.HumanResources.TaxRates.Commands.UpdateTaxRate;
using NexaSoft.Club.Application.HumanResources.TaxRates.Commands.DeleteTaxRate;
using NexaSoft.Club.Application.HumanResources.TaxRates.Queries.GetTaxRate;
using NexaSoft.Club.Application.HumanResources.TaxRates.Queries.GetTaxRates;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.TaxRates;

[Route("api/[controller]")]
[ApiController]
public class TaxRateController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateTaxRate(CreateTaxRateRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateTaxRateCommand(
             request.Code,
             request.Name,
             request.RateValue,
             request.RateType,
             request.MinAmount,
             request.MaxAmount,
             request.EffectiveDate,
             request.EndDate,
             request.Category,
             request.Description,
             request.AppliesTo,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateTaxRate(UpdateTaxRateRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateTaxRateCommand(
           request.Id,
             request.Code,
             request.Name,
             request.RateValue,
             request.RateType,
             request.MinAmount,
             request.MaxAmount,
             request.EffectiveDate,
             request.EndDate,
             request.Category,
             request.Description,
             request.AppliesTo,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeleteTaxRate(DeleteTaxRateRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteTaxRateCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetTaxRates(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetTaxRatesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetTaxRate(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetTaxRateQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
