using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.CostCenterTypes.Request;
using NexaSoft.Club.Application.HumanResources.CostCenterTypes.Commands.CreateCostCenterType;
using NexaSoft.Club.Application.HumanResources.CostCenterTypes.Commands.UpdateCostCenterType;
using NexaSoft.Club.Application.HumanResources.CostCenterTypes.Commands.DeleteCostCenterType;
using NexaSoft.Club.Application.HumanResources.CostCenterTypes.Queries.GetCostCenterType;
using NexaSoft.Club.Application.HumanResources.CostCenterTypes.Queries.GetCostCenterTypes;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using NexaSoft.Club.Api.Attributes;

namespace NexaSoft.Club.Api.Controllers.HumanResources.CostCenterTypes;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CostCenterTypeController(ISender _sender) : ControllerBase
{

    [HttpPost]
    [GeneratePermission]
    [RequirePermission("CostCenterType.CreateCostCenterType")]
    public async Task<IActionResult> CreateCostCenterType(CreateCostCenterTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCostCenterTypeCommand(
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
    [RequirePermission("CostCenterType.UpdateCostCenterType")]
    public async Task<IActionResult> UpdateCostCenterType(UpdateCostCenterTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateCostCenterTypeCommand(
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
    [RequirePermission("CostCenterType.DeleteCostCenterType")]
    public async Task<IActionResult> DeleteCostCenterType(DeleteCostCenterTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteCostCenterTypeCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    [GeneratePermission]
    [RequirePermission("CostCenterType.GetCostCenterType")]
    public async Task<IActionResult> GetCostCenterTypes(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetCostCenterTypesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    [GeneratePermission]
    [RequirePermission("CostCenterType.GetCostCenterType")]
    public async Task<IActionResult> GetCostCenterType(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetCostCenterTypeQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
