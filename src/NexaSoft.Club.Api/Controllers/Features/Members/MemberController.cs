using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Features.Members.Request;
using NexaSoft.Club.Application.Features.Members.Commands.CreateMember;
using NexaSoft.Club.Application.Features.Members.Commands.UpdateMember;
using NexaSoft.Club.Application.Features.Members.Commands.DeleteMember;
using NexaSoft.Club.Application.Features.Members.Queries.GetMember;
using NexaSoft.Club.Application.Features.Members.Queries.GetMembers;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using NexaSoft.Club.Application.Features.Members.Queries.GetMemberMetrics;
using NexaSoft.Club.Api.Controllers.Features.Members.Requests;
using NexaSoft.Club.Application.Features.Members.Queries.GetMemberPasswordStatus;
using NexaSoft.Club.Application.Features.Members.Queries.GetMemberQr;
using NexaSoft.Club.Api.Attributes;

namespace NexaSoft.Club.Api.Controllers.Features.Members;

using Microsoft.AspNetCore.Authorization;
using NexaSoft.Club.Api.Attributes;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MemberController(ISender _sender) : ControllerBase
{
    [HttpPost]
    [RequirePermission("Member.CreateMember")]
    public async Task<IActionResult> CreateMember(CreateMemberRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateMemberCommand(
             request.Dni,
             request.FirstName,
             request.LastName,
             request.Email,
             request.Phone,
             request.DepartamentId,
             request.ProvinceId,
             request.DistrictId,
             request.Address,
             request.BirthDate,
             request.MemberTypeId,
             request.StatusId,
             request.JoinDate,
             request.ExpirationDate,
             request.Balance,
             request.UserTypeId,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);
        return resultado.ToActionResult(this);
    }

    [HttpPut]
    [RequirePermission("Member.UpdateMember")]
    public async Task<IActionResult> UpdateMember(UpdateMemberRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateMemberCommand(
             request.Id,
             request.Dni,
             request.FirstName,
             request.LastName,
             request.Email,
             request.Phone,
             request.DepartamentId,
             request.ProvinceId,
             request.DistrictId,
             request.Address,
             request.BirthDate,
             request.Balance,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);
        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    [RequirePermission("Member.DeleteMember")]
    public async Task<IActionResult> DeleteMember(DeleteMemberRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteMemberCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    [GeneratePermission]
    [RequirePermission("Member.GetMember")]
    public async Task<IActionResult> GetMembers(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetMembersQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    [GeneratePermission]
    [RequirePermission("Member.GetMember")]
    public async Task<IActionResult> GetMember(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetMemberQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet("qr/{memberId:long}")]
    [GeneratePermission]
    [RequirePermission("Member.GetMember")]
    public async Task<IActionResult> GetMemberQr(
        long memberId,
        CancellationToken cancellationToken
    )
    {
        var query = new GetMemberQrQuery(memberId);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet("metrics/{memberId:long}")]
    [GeneratePermission]
    [RequirePermission("Member.GetMember")]
    public async Task<IActionResult> GetMemberMetrics(
        long memberId,
        CancellationToken cancellationToken
     )
    {
        var query = new GetMemberMetricQuery(memberId);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPost("has-password")]
    [GeneratePermission]
    [RequirePermission("Member.GetMember")]
    public async Task<IActionResult> HasPassword(HasPasswordRequest request, CancellationToken cancellationToken)
    {
        var query = new GetMemberPasswordStatusQuery(
            request.Dni,
            request.BirthDate
        );
        var resultado = await _sender.Send(query, cancellationToken);
        return resultado.ToActionResult(this);
    }

}
