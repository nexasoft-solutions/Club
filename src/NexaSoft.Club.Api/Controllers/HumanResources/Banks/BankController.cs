using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.Banks.Request;
using NexaSoft.Club.Application.HumanResources.Banks.Commands.CreateBank;
using NexaSoft.Club.Application.HumanResources.Banks.Commands.UpdateBank;
using NexaSoft.Club.Application.HumanResources.Banks.Commands.DeleteBank;
using NexaSoft.Club.Application.HumanResources.Banks.Queries.GetBank;
using NexaSoft.Club.Application.HumanResources.Banks.Queries.GetBanks;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.Banks;

[Route("api/[controller]")]
[ApiController]
public class BankController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateBank(CreateBankRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateBankCommand(
             request.Code,
             request.Name,
             request.WebSite,
             request.Phone,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateBank(UpdateBankRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateBankCommand(
           request.Id,
             request.Code,
             request.Name,
             request.WebSite,
             request.Phone,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeleteBank(DeleteBankRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteBankCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetBanks(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetBanksQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetBank(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetBankQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
