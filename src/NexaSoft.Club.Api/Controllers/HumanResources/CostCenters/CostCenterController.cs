using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.CostCenters.Request;
using NexaSoft.Club.Application.HumanResources.CostCenters.Commands.CreateCostCenter;
using NexaSoft.Club.Application.HumanResources.CostCenters.Commands.UpdateCostCenter;
using NexaSoft.Club.Application.HumanResources.CostCenters.Commands.DeleteCostCenter;
using NexaSoft.Club.Application.HumanResources.CostCenters.Queries.GetCostCenter;
using NexaSoft.Club.Application.HumanResources.CostCenters.Queries.GetCostCenters;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.CostCenters;

[Route("api/[controller]")]
[ApiController]
public class CostCenterController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateCostCenter(CreateCostCenterRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCostCenterCommand(
             request.Code,
             request.Name,
             request.ParentCostCenterId,
             request.CostCenterTypeId,
             request.Description,
             request.ResponsibleId,
             request.Budget,
             request.StartDate,
             request.EndDate,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCostCenter(UpdateCostCenterRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateCostCenterCommand(
           request.Id,
             request.Code,
             request.Name,
             request.ParentCostCenterId,
             request.CostCenterTypeId,
             request.Description,
             request.ResponsibleId,
             request.Budget,
             request.StartDate,
             request.EndDate,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCostCenter(DeleteCostCenterRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteCostCenterCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetCostCenters(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetCostCentersQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetCostCenter(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetCostCenterQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
