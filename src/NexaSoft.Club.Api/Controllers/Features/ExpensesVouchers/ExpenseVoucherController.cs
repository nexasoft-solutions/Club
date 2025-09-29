using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Features.ExpensesVouchers.Request;
using NexaSoft.Club.Application.Features.ExpensesVouchers.Commands.CreateExpenseVoucher;
using NexaSoft.Club.Application.Features.ExpensesVouchers.Commands.UpdateExpenseVoucher;
using NexaSoft.Club.Application.Features.ExpensesVouchers.Commands.DeleteExpenseVoucher;
using NexaSoft.Club.Application.Features.ExpensesVouchers.Queries.GetExpenseVoucher;
using NexaSoft.Club.Application.Features.ExpensesVouchers.Queries.GetExpensesVouchers;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.Features.ExpensesVouchers;

[Route("api/[controller]")]
[ApiController]
public class ExpenseVoucherController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateExpenseVoucher(CreateExpenseVoucherRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateExpenseVoucherCommand(
             request.EntryId,
             request.VoucherNumber,
             request.SupplierName,
             request.Amount,
             request.IssueDate,
             request.Description,
             request.ExpenseAccountId,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateExpenseVoucher(UpdateExpenseVoucherRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateExpenseVoucherCommand(
             request.Id,
             request.EntryId,
             request.VoucherNumber,
             request.SupplierName,
             request.Amount,
             request.IssueDate,
             request.Description,
             request.ExpenseAccountId,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeleteExpenseVoucher(DeleteExpenseVoucherRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteExpenseVoucherCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetExpensesVouchers(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetExpensesVouchersQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetExpenseVoucher(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetExpenseVoucherQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
