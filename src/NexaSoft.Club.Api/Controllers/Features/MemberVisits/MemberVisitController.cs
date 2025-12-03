using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Features.MemberVisits.Request;
using NexaSoft.Club.Application.Features.MemberVisits.Commands.CreateMemberVisit;
using NexaSoft.Club.Application.Features.MemberVisits.Commands.UpdateMemberVisit;
using NexaSoft.Club.Application.Features.MemberVisits.Commands.DeleteMemberVisit;
using NexaSoft.Club.Application.Features.MemberVisits.Queries.GetMemberVisit;
using NexaSoft.Club.Application.Features.MemberVisits.Queries.GetMemberVisits;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using NexaSoft.Club.Application.Features.MemberVisits.Commands.ExitVisitMember;
using NexaSoft.Club.Api.Controllers.Features.MemberVisits.Requests;
using NexaSoft.Club.Api.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace NexaSoft.Club.Api.Controllers.Features.MemberVisits;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MemberVisitController(ISender _sender) : ControllerBase
{

    [HttpPost]
    [GeneratePermission]
    [RequirePermission("MemberVisit.CreateMemberVisit")]
    public async Task<IActionResult> CreateMemberVisit(CreateMemberVisitRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateMemberVisitCommand(
             request.MemberId,
             request.QrCodeUsed,
             request.Notes,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPatch("exit-visit")]
    [GeneratePermission]
    [RequirePermission("MemberVisit.ExitVisitMember")]
    public async Task<IActionResult> ExitVisitMember(ExitVisitMemberRequest request, CancellationToken cancellationToken)
    {
        var command = new ExitVisitMemberCommand(
            request.MemberId,
            request.CheckOutBy,
            request.Notes
        );
        
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    [GeneratePermission]
    [RequirePermission("MemberVisit.UpdateMemberVisit")]
    public async Task<IActionResult> UpdateMemberVisit(UpdateMemberVisitRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateMemberVisitCommand(
             request.Id,
             request.MemberId,
             request.VisitDate,
             request.EntryTime,
             request.ExitTime,
             request.QrCodeUsed,
             request.Notes,
             request.CheckInBy,
             request.CheckOutBy,
             request.VisitType,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    [GeneratePermission]
    [RequirePermission("MemberVisit.DeleteMemberVisit")]
    public async Task<IActionResult> DeleteMemberVisit(DeleteMemberVisitRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteMemberVisitCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    [GeneratePermission]
    [RequirePermission("MemberVisit.GetMemberVisits")]
    public async Task<IActionResult> GetMemberVisits(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetMemberVisitsQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    [GeneratePermission]
    [RequirePermission("MemberVisit.GetMemberVisit")]
    public async Task<IActionResult> GetMemberVisit(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetMemberVisitQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
