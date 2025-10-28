using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Features.MemberFees.Request;
using NexaSoft.Club.Application.Features.MemberFees.Commands.CreateMemberFee;
using NexaSoft.Club.Application.Features.MemberFees.Commands.UpdateMemberFee;
using NexaSoft.Club.Application.Features.MemberFees.Commands.DeleteMemberFee;
using NexaSoft.Club.Application.Features.MemberFees.Queries.GetMemberFee;
using NexaSoft.Club.Application.Features.MemberFees.Queries.GetMemberFees;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using NexaSoft.Club.Api.Controllers.Features.MemberFees.Requests;
using NexaSoft.Club.Application.Features.MemberFees.Queries.GetMemberFeesStatus;

namespace NexaSoft.Club.Api.Controllers.Features.MemberFees;

[Route("api/[controller]")]
[ApiController]
public class MemberFeeController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateMemberFee(CreateMemberFeeRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateMemberFeeCommand(
             request.MemberId,
             request.ConfigId,
             request.Period,
             request.Amount,
             request.DueDate,
             request.StatusId,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateMemberFee(UpdateMemberFeeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateMemberFeeCommand(
           request.Id,
             request.MemberId,
             request.ConfigId,
             request.Period,
             request.Amount,
             request.DueDate,
             request.StatusId,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteMemberFee(DeleteMemberFeeRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteMemberFeeCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetMemberFees(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetMemberFeesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetMemberFee(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetMemberFeeQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet("status")]
    public async Task<IActionResult> GetMemberFeesStatus(
        [FromQuery] GetMemberFeesStatusRequest request,
        CancellationToken cancellationToken
    )
    {
        var query = new GetMemberFeesStatusQuery(
            request.MemberId,
            request.StatusIds,
            request.OrderBy
        );
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
