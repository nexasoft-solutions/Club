using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Features.EntryItems.Request;
using NexaSoft.Club.Application.Features.EntryItems.Commands.CreateEntryItem;
using NexaSoft.Club.Application.Features.EntryItems.Commands.UpdateEntryItem;
using NexaSoft.Club.Application.Features.EntryItems.Commands.DeleteEntryItem;
using NexaSoft.Club.Application.Features.EntryItems.Queries.GetEntryItem;
using NexaSoft.Club.Application.Features.EntryItems.Queries.GetEntryItems;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.Features.EntryItems;

[Route("api/[controller]")]
[ApiController]
public class EntryItemController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateEntryItem(CreateEntryItemRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateEntryItemCommand(
             request.EntryId,
             request.AccountId,
             request.DebitAmount,
             request.CreditAmount,
             request.Description,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateEntryItem(UpdateEntryItemRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateEntryItemCommand(
           request.Id,
             request.EntryId,
             request.AccountId,
             request.DebitAmount,
             request.CreditAmount,
             request.Description,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeleteEntryItem(DeleteEntryItemRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteEntryItemCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetEntryItems(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetEntryItemsQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetEntryItem(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetEntryItemQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
