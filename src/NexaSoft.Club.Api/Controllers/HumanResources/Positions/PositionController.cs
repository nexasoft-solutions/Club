using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.Positions.Request;
using NexaSoft.Club.Application.HumanResources.Positions.Commands.CreatePosition;
using NexaSoft.Club.Application.HumanResources.Positions.Commands.UpdatePosition;
using NexaSoft.Club.Application.HumanResources.Positions.Commands.DeletePosition;
using NexaSoft.Club.Application.HumanResources.Positions.Queries.GetPosition;
using NexaSoft.Club.Application.HumanResources.Positions.Queries.GetPositions;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.Positions;

[Route("api/[controller]")]
[ApiController]
public class PositionController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreatePosition(CreatePositionRequest request, CancellationToken cancellationToken)
    {
        var command = new CreatePositionCommand(
             request.Code,
             request.Name,
             request.DepartmentId,
             request.EmployeeTypeId,
             request.BaseSalary,
             request.Description,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdatePosition(UpdatePositionRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdatePositionCommand(
           request.Id,
             request.Code,
             request.Name,
             request.DepartmentId,
             request.EmployeeTypeId,
             request.BaseSalary,
             request.Description,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeletePosition(DeletePositionRequest request, CancellationToken cancellationToken)
    {
        var command = new DeletePositionCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetPositions(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPositionsQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetPosition(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPositionQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
