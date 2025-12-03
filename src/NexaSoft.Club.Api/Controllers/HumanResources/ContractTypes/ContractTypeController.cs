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
using Microsoft.AspNetCore.Authorization;
using NexaSoft.Club.Api.Attributes;

namespace NexaSoft.Club.Api.Controllers.HumanResources.ContractTypes;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ContractTypeController(ISender _sender) : ControllerBase
{

    [HttpPost]
    [GeneratePermission]
    [RequirePermission("ContractType.CreateContractType")]
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
    [GeneratePermission]
    [RequirePermission("ContractType.UpdateContractType")]
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
    [GeneratePermission]
    [RequirePermission("ContractType.DeleteContractType")]
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
    [GeneratePermission]
    [RequirePermission("ContractType.GetContractType")]
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
    [GeneratePermission]
    [RequirePermission("ContractType.GetContractType")]
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
