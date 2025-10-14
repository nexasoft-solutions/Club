using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Masters.AccountTypes.Request;
using NexaSoft.Club.Application.Masters.AccountTypes.Commands.CreateAccountType;
using NexaSoft.Club.Application.Masters.AccountTypes.Commands.UpdateAccountType;
using NexaSoft.Club.Application.Masters.AccountTypes.Commands.DeleteAccountType;
using NexaSoft.Club.Application.Masters.AccountTypes.Queries.GetAccountType;
using NexaSoft.Club.Application.Masters.AccountTypes.Queries.GetAccountTypes;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.Masters.AccountTypes;

[Route("api/[controller]")]
[ApiController]
public class AccountTypeController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateAccountType(CreateAccountTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateAccountTypeCommand(
             request.Name,
             request.Description,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateAccountType(UpdateAccountTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateAccountTypeCommand(
           request.Id,
             request.Name,
             request.Description,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeleteAccountType(DeleteAccountTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteAccountTypeCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetAccountTypes(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetAccountTypesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetAccountType(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetAccountTypeQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
