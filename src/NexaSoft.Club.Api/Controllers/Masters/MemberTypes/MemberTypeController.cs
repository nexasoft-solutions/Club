using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Masters.MemberTypes.Request;
using NexaSoft.Club.Application.Masters.MemberTypes.Commands.CreateMemberType;
using NexaSoft.Club.Application.Masters.MemberTypes.Commands.UpdateMemberType;
using NexaSoft.Club.Application.Masters.MemberTypes.Commands.DeleteMemberType;
using NexaSoft.Club.Application.Masters.MemberTypes.Queries.GetMemberType;
using NexaSoft.Club.Application.Masters.MemberTypes.Queries.GetMemberTypes;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using NexaSoft.Club.Api.Attributes;

namespace NexaSoft.Club.Api.Controllers.Masters.MemberTypes;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MemberTypeController(ISender _sender) : ControllerBase
{

    [HttpPost]
    [GeneratePermission]
    [RequirePermission("MemberType.CreateMemberType")]
    public async Task<IActionResult> CreateMemberType(CreateMemberTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateMemberTypeCommand(
             request.TypeName,
             request.Description,
             request.HasFamilyDiscount,
             request.FamilyDiscountRate,
             request.MaxFamilyMembers,
             request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateMemberType(UpdateMemberTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateMemberTypeCommand(
             request.Id,
             request.TypeName,
             request.Description,
             request.HasFamilyDiscount,
             request.FamilyDiscountRate,
             request.MaxFamilyMembers,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    [GeneratePermission]
    [RequirePermission("MemberType.DeleteMemberType")]
    public async Task<IActionResult> DeleteMemberType(DeleteMemberTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteMemberTypeCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    [GeneratePermission]
    [RequirePermission("MemberType.GetMemberType")]
    public async Task<IActionResult> GetMemberTypes(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetMemberTypesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    [GeneratePermission]
    [RequirePermission("MemberType.GetMemberType")]
    public async Task<IActionResult> GetMemberType(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetMemberTypeQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
