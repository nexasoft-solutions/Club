using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.PayrollStatusTypes.Request;
using NexaSoft.Club.Application.HumanResources.PayrollStatusTypes.Commands.CreatePayrollStatusType;
using NexaSoft.Club.Application.HumanResources.PayrollStatusTypes.Commands.UpdatePayrollStatusType;
using NexaSoft.Club.Application.HumanResources.PayrollStatusTypes.Commands.DeletePayrollStatusType;
using NexaSoft.Club.Application.HumanResources.PayrollStatusTypes.Queries.GetPayrollStatusType;
using NexaSoft.Club.Application.HumanResources.PayrollStatusTypes.Queries.GetPayrollStatusTypes;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollStatusTypes;

[Route("api/[controller]")]
[ApiController]
public class PayrollStatusTypeController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreatePayrollStatusType(CreatePayrollStatusTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new CreatePayrollStatusTypeCommand(
             request.Code,
             request.Name,
             request.Description,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdatePayrollStatusType(UpdatePayrollStatusTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdatePayrollStatusTypeCommand(
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
   public async Task<IActionResult> DeletePayrollStatusType(DeletePayrollStatusTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new DeletePayrollStatusTypeCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetPayrollStatusTypes(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPayrollStatusTypesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetPayrollStatusType(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetPayrollStatusTypeQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
