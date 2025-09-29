using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Features.AccountingEntries.Request;
using NexaSoft.Club.Application.Features.AccountingEntries.Commands.CreateAccountingEntry;
using NexaSoft.Club.Application.Features.AccountingEntries.Commands.UpdateAccountingEntry;
using NexaSoft.Club.Application.Features.AccountingEntries.Commands.DeleteAccountingEntry;
using NexaSoft.Club.Application.Features.AccountingEntries.Queries.GetAccountingEntry;
using NexaSoft.Club.Application.Features.AccountingEntries.Queries.GetAccountingEntries;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.Features.AccountingEntries;

[Route("api/[controller]")]
[ApiController]
public class AccountingEntryController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateAccountingEntry(CreateAccountingEntryRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateAccountingEntryCommand(
             request.EntryNumber,
             request.EntryDate,
             request.Description,
             request.sourceModule,
             request.sourceId,
             request.TotalDebit,
             request.TotalCredit,
             request.isAdjusted,
             request.adjustmentReason,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAccountingEntry(UpdateAccountingEntryRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateAccountingEntryCommand(
           request.Id,
             request.EntryNumber,
             request.EntryDate,
             request.Description,
             request.TotalDebit,
             request.TotalCredit,
             request.IsAdjusted,
             request.AdjustmentReason,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAccountingEntry(DeleteAccountingEntryRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteAccountingEntryCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetAccountingEntries(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetAccountingEntriesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetAccountingEntry(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetAccountingEntryQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
