using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.SpecialRates.Request;
using NexaSoft.Club.Application.HumanResources.SpecialRates.Commands.CreateSpecialRate;
using NexaSoft.Club.Application.HumanResources.SpecialRates.Commands.UpdateSpecialRate;
using NexaSoft.Club.Application.HumanResources.SpecialRates.Commands.DeleteSpecialRate;
using NexaSoft.Club.Application.HumanResources.SpecialRates.Queries.GetSpecialRate;
using NexaSoft.Club.Application.HumanResources.SpecialRates.Queries.GetSpecialRates;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using NexaSoft.Club.Api.Attributes;

namespace NexaSoft.Club.Api.Controllers.HumanResources.SpecialRates;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SpecialRateController(ISender _sender) : ControllerBase
{

    [HttpPost]
    [GeneratePermission]
    [RequirePermission("SpecialRate.CreateSpecialRate")]
    public async Task<IActionResult> CreateSpecialRate(CreateSpecialRateRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateSpecialRateCommand(
             request.RateTypeId,
             request.Name,
             request.Multiplier,
             request.StartTime,
             request.EndTime,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    [GeneratePermission]
    [RequirePermission("SpecialRate.UpdateSpecialRate")]
    public async Task<IActionResult> UpdateSpecialRate(UpdateSpecialRateRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateSpecialRateCommand(
             request.Id,
             request.RateTypeId,
             request.Name,
             request.Multiplier,
             request.StartTime,
             request.EndTime,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    [GeneratePermission]
    [RequirePermission("SpecialRate.DeleteSpecialRate")]
    public async Task<IActionResult> DeleteSpecialRate(DeleteSpecialRateRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteSpecialRateCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    [GeneratePermission]
    [RequirePermission("SpecialRate.GetSpecialRate")]
    public async Task<IActionResult> GetSpecialRates(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetSpecialRatesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    [GeneratePermission]
    [RequirePermission("SpecialRate.GetSpecialRate")]
    public async Task<IActionResult> GetSpecialRate(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetSpecialRateQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
