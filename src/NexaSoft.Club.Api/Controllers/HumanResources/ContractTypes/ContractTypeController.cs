using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.ContractTypes.Request;
using NexaSoft.Club.Application.HumanResources.ContractTypes.Commands.CreateContractType;
using NexaSoft.Club.Application.HumanResources.ContractTypes.Commands.UpdateContractType;
using NexaSoft.Club.Application.HumanResources.ContractTypes.Commands.DeleteContractType;
using NexaSoft.Club.Application.HumanResources.ContractTypes.Queries.GetContractType;
using NexaSoft.Club.Application.HumanResources.ContractTypes.Queries.GetContractTypes;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.ContractTypes;

[Route("api/[controller]")]
[ApiController]
public class ContractTypeController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateContractType(CreateContractTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateContractTypeCommand(
             request.Code,
             request.Name,
             request.Description,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateContractType(UpdateContractTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateContractTypeCommand(
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
   public async Task<IActionResult> DeleteContractType(DeleteContractTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteContractTypeCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetContractTypes(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetContractTypesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetContractType(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetContractTypeQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
