using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.EmployeesInfo.Request;
using NexaSoft.Club.Application.HumanResources.EmployeesInfo.Commands.CreateEmployeeInfo;
using NexaSoft.Club.Application.HumanResources.EmployeesInfo.Commands.UpdateEmployeeInfo;
using NexaSoft.Club.Application.HumanResources.EmployeesInfo.Commands.DeleteEmployeeInfo;
using NexaSoft.Club.Application.HumanResources.EmployeesInfo.Queries.GetEmployeeInfo;
using NexaSoft.Club.Application.HumanResources.EmployeesInfo.Queries.GetEmployeesInfo;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.EmployeesInfo;

[Route("api/[controller]")]
[ApiController]
public class EmployeeInfoController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateEmployeeInfo(CreateEmployeeInfoRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateEmployeeInfoCommand(
             request.EmployeeCode,
             request.UserId,
             request.PositionId,
             request.EmployeeTypeId,
             request.DepartmentId,
             request.HireDate,
             request.BaseSalary,
             request.PaymentMethodId,
             request.BankId,
             request.BankAccountTypeId,
             request.CurrencyId,
             request.BankAccountNumber,
             request.CciNumber,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateEmployeeInfo(UpdateEmployeeInfoRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateEmployeeInfoCommand(
           request.Id,
             request.EmployeeCode,
             request.UserId,
             request.PositionId,
             request.EmployeeTypeId,
             request.DepartmentId,
             request.HireDate,
             request.BaseSalary,
             request.PaymentMethodId,
             request.BankId,
             request.BankAccountTypeId,
             request.CurrencyId,
             request.BankAccountNumber,
             request.CciNumber,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEmployeeInfo(DeleteEmployeeInfoRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteEmployeeInfoCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployeesInfo(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetEmployeesInfoQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetEmployeeInfo(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetEmployeeInfoQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
