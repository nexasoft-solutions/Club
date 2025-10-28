using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.BankAccountTypes.Request;
using NexaSoft.Club.Application.HumanResources.BankAccountTypes.Commands.CreateBankAccountType;
using NexaSoft.Club.Application.HumanResources.BankAccountTypes.Commands.UpdateBankAccountType;
using NexaSoft.Club.Application.HumanResources.BankAccountTypes.Commands.DeleteBankAccountType;
using NexaSoft.Club.Application.HumanResources.BankAccountTypes.Queries.GetBankAccountType;
using NexaSoft.Club.Application.HumanResources.BankAccountTypes.Queries.GetBankAccountTypes;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.BankAccountTypes;

[Route("api/[controller]")]
[ApiController]
public class BankAccountTypeController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateBankAccountType(CreateBankAccountTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateBankAccountTypeCommand(
             request.Code,
             request.Name,
             request.Description,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateBankAccountType(UpdateBankAccountTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateBankAccountTypeCommand(
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
   public async Task<IActionResult> DeleteBankAccountType(DeleteBankAccountTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteBankAccountTypeCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetBankAccountTypes(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetBankAccountTypesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetBankAccountType(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetBankAccountTypeQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
