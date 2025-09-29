using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Features.FamilyMembers.Request;
using NexaSoft.Club.Application.Features.FamilyMembers.Commands.CreateFamilyMember;
using NexaSoft.Club.Application.Features.FamilyMembers.Commands.UpdateFamilyMember;
using NexaSoft.Club.Application.Features.FamilyMembers.Commands.DeleteFamilyMember;
using NexaSoft.Club.Application.Features.FamilyMembers.Queries.GetFamilyMember;
using NexaSoft.Club.Application.Features.FamilyMembers.Queries.GetFamilyMembers;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.Features.FamilyMembers;

[Route("api/[controller]")]
[ApiController]
public class FamilyMemberController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateFamilyMember(CreateFamilyMemberRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateFamilyMemberCommand(
             request.MemberId,
             request.Dni,
             request.FirstName,
             request.LastName,
             request.Relationship,
             request.BirthDate,
             request.IsAuthorized,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateFamilyMember(UpdateFamilyMemberRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateFamilyMemberCommand(
           request.Id,
             request.MemberId,
             request.Dni,
             request.FirstName,
             request.LastName,
             request.Relationship,
             request.BirthDate,
             request.IsAuthorized,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteFamilyMember(DeleteFamilyMemberRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteFamilyMemberCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetFamilyMembers(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetFamilyMembersQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetFamilyMember(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetFamilyMemberQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
