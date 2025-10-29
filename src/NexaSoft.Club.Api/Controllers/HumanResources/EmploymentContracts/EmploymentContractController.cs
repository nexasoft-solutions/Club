using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.EmploymentContracts.Request;
using NexaSoft.Club.Application.HumanResources.EmploymentContracts.Commands.CreateEmploymentContract;
using NexaSoft.Club.Application.HumanResources.EmploymentContracts.Commands.UpdateEmploymentContract;
using NexaSoft.Club.Application.HumanResources.EmploymentContracts.Commands.DeleteEmploymentContract;
using NexaSoft.Club.Application.HumanResources.EmploymentContracts.Queries.GetEmploymentContract;
using NexaSoft.Club.Application.HumanResources.EmploymentContracts.Queries.GetEmploymentContracts;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.EmploymentContracts;

[Route("api/[controller]")]
[ApiController]
public class EmploymentContractController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateEmploymentContract(CreateEmploymentContractRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateEmploymentContractCommand(
             request.EmployeeId,
             request.ContractTypeId,
             request.StartDate,
             request.EndDate,
             request.Salary,
             request.WorkingHours,
             request.DocumentPath,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateEmploymentContract(UpdateEmploymentContractRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateEmploymentContractCommand(
           request.Id,
             request.EmployeeId,
             request.ContractTypeId,
             request.StartDate,
             request.EndDate,
             request.Salary,
             request.WorkingHours,
             request.DocumentPath,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeleteEmploymentContract(DeleteEmploymentContractRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteEmploymentContractCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetEmploymentContracts(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetEmploymentContractsQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetEmploymentContract(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetEmploymentContractQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
