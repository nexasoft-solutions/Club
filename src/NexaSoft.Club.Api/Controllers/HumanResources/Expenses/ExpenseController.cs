using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.HumanResources.Expenses.Request;
using NexaSoft.Club.Application.HumanResources.Expenses.Commands.CreateExpense;
using NexaSoft.Club.Application.HumanResources.Expenses.Commands.UpdateExpense;
using NexaSoft.Club.Application.HumanResources.Expenses.Commands.DeleteExpense;
using NexaSoft.Club.Application.HumanResources.Expenses.Queries.GetExpense;
using NexaSoft.Club.Application.HumanResources.Expenses.Queries.GetExpenses;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.HumanResources.Expenses;

[Route("api/[controller]")]
[ApiController]
public class ExpenseController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateExpense(CreateExpenseRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateExpenseCommand(
             request.CostCenterId,
             request.Description,
             request.ExpenseDate,
             request.Amount,
             request.DocumentNumber,
             request.DocumentPath,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateExpense(UpdateExpenseRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateExpenseCommand(
           request.Id,
             request.CostCenterId,
             request.Description,
             request.ExpenseDate,
             request.Amount,
             request.DocumentNumber,
             request.DocumentPath,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeleteExpense(DeleteExpenseRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteExpenseCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetExpenses(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetExpensesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetExpense(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetExpenseQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
