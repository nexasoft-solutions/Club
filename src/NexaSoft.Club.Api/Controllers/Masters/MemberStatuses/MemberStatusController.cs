using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Masters.MemberStatuses.Request;
using NexaSoft.Club.Application.Masters.MemberStatuses.Commands.CreateMemberStatus;
using NexaSoft.Club.Application.Masters.MemberStatuses.Commands.UpdateMemberStatus;
using NexaSoft.Club.Application.Masters.MemberStatuses.Commands.DeleteMemberStatus;
using NexaSoft.Club.Application.Masters.MemberStatuses.Queries.GetMemberStatus;
using NexaSoft.Club.Application.Masters.MemberStatuses.Queries.GetMemberStatuses;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.Masters.MemberStatuses;

[Route("api/[controller]")]
[ApiController]
public class MemberStatusController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateMemberStatus(CreateMemberStatusRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateMemberStatusCommand(
             request.StatusName,
             request.Description,
             request.CanAccess,
             request.CanReserve,
             request.CanParticipate,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateMemberStatus(UpdateMemberStatusRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateMemberStatusCommand(
           request.Id,
             request.StatusName,
             request.Description,
             request.CanAccess,
             request.CanReserve,
             request.CanParticipate,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeleteMemberStatus(DeleteMemberStatusRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteMemberStatusCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetMemberStatuses(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetMemberStatusesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetMemberStatus(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetMemberStatusQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
