using MediatR;
using Microsoft.AspNetCore.Mvc;

using NexaSoft.Club.Application.Features.AccountingEntries.Queries.GetAccountingEntry;
using NexaSoft.Club.Application.Features.AccountingEntries.Queries.GetAccountingEntries;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using NexaSoft.Club.Api.Controllers.Features.AccountingEntries.Requests;
using NexaSoft.Club.Application.Features.AccountingEntries.Queries.GetAccountingEntriesBySource;

namespace NexaSoft.Club.Api.Controllers.Features.AccountingEntries;

[Route("api/[controller]")]
[ApiController]
public class AccountingEntryController(ISender _sender) : ControllerBase
{

    
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

    [HttpGet("by-source")]
    public async Task<IActionResult> GetAccountingEntriesBySource(
        [FromQuery] GetAccountingEntryBySourceRequest request,
        CancellationToken cancellationToken
    )
    {
        var query = new GetAccountingEntriesBySourceQuery(request.SourceModuleId, request.SourceId);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
