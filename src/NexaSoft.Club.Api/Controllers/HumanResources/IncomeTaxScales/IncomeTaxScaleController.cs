using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.IncomeTaxScales.Request;
using NexaSoft.Club.Application.HumanResources.IncomeTaxScales.Commands.CreateIncomeTaxScale;
using NexaSoft.Club.Application.HumanResources.IncomeTaxScales.Commands.UpdateIncomeTaxScale;
using NexaSoft.Club.Application.HumanResources.IncomeTaxScales.Commands.DeleteIncomeTaxScale;
using NexaSoft.Club.Application.HumanResources.IncomeTaxScales.Queries.GetIncomeTaxScale;
using NexaSoft.Club.Application.HumanResources.IncomeTaxScales.Queries.GetIncomeTaxScales;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.IncomeTaxScales;

[Route("api/[controller]")]
[ApiController]
public class IncomeTaxScaleController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateIncomeTaxScale(CreateIncomeTaxScaleRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateIncomeTaxScaleCommand(
             request.ScaleYear,
             request.MinIncome,
             request.MaxIncome,
             request.FixedAmount,
             request.Rate,
             request.ExcessOver,
             request.Description,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateIncomeTaxScale(UpdateIncomeTaxScaleRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateIncomeTaxScaleCommand(
           request.Id,
             request.ScaleYear,
             request.MinIncome,
             request.MaxIncome,
             request.FixedAmount,
             request.Rate,
             request.ExcessOver,
             request.Description,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeleteIncomeTaxScale(DeleteIncomeTaxScaleRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteIncomeTaxScaleCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetIncomeTaxScales(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetIncomeTaxScalesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetIncomeTaxScale(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetIncomeTaxScaleQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
