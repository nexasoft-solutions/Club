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

namespace NexaSoft.Club.Api.Controllers.Features.Members;

[Route("api/[controller]")]
[ApiController]
public class MemberController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateMember(CreateMemberRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateMemberCommand(
             request.Dni,
             request.FirstName,
             request.LastName,
             request.Email,
             request.Phone,
             request.Address,
             request.BirthDate,
             request.MemberTypeId,
             request.StatusId,
             request.JoinDate,
             request.ExpirationDate,
             request.Balance,
             request.QrCode,
             request.QrExpiration,
             request.ProfilePictureUrl,           
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateMember(UpdateMemberRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateMemberCommand(
           request.Id,
             request.Dni,
             request.FirstName,
             request.LastName,
             request.Email,
             request.Phone,
             request.Address,
             request.BirthDate,
             request.MemberTypeId,
             request.StatusId,
             request.JoinDate,
             request.ExpirationDate,
             request.Balance,
             request.QrCode,
             request.QrExpiration,
             request.ProfilePictureUrl,
             request.EntryFeePaid,
             request.LastPaymentDate,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
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
   public async Task<IActionResult> GetMember(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetMemberQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
