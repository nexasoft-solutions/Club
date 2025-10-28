using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.Currencies.Request;
using NexaSoft.Club.Application.HumanResources.Currencies.Commands.CreateCurrency;
using NexaSoft.Club.Application.HumanResources.Currencies.Commands.UpdateCurrency;
using NexaSoft.Club.Application.HumanResources.Currencies.Commands.DeleteCurrency;
using NexaSoft.Club.Application.HumanResources.Currencies.Queries.GetCurrency;
using NexaSoft.Club.Application.HumanResources.Currencies.Queries.GetCurrencies;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.Currencies;

[Route("api/[controller]")]
[ApiController]
public class CurrencyController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateCurrency(CreateCurrencyRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCurrencyCommand(
             request.Code,
             request.Name,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateCurrency(UpdateCurrencyRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateCurrencyCommand(
           request.Id,
             request.Code,
             request.Name,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeleteCurrency(DeleteCurrencyRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteCurrencyCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetCurrencies(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetCurrenciesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetCurrency(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetCurrencyQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
