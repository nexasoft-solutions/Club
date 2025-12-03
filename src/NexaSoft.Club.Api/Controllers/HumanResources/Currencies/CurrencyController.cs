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
using Microsoft.AspNetCore.Authorization;
using NexaSoft.Club.Api.Attributes;

namespace NexaSoft.Club.Api.Controllers.HumanResources.Currencies;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CurrencyController(ISender _sender) : ControllerBase
{

    [HttpPost]
    [GeneratePermission]
    [RequirePermission("Currency.CreateCurrency")]
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
    [GeneratePermission]
    [RequirePermission("Currency.UpdateCurrency")]
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
    [GeneratePermission]
    [RequirePermission("Currency.DeleteCurrency")]
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
    [GeneratePermission]
    [RequirePermission("Currency.GetCurrency")]
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
    [GeneratePermission]
    [RequirePermission("Currency.GetCurrency")]
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
