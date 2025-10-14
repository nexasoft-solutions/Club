using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Masters.UserTypes.Request;
using NexaSoft.Club.Application.Masters.UserTypes.Commands.CreateUserType;
using NexaSoft.Club.Application.Masters.UserTypes.Commands.UpdateUserType;
using NexaSoft.Club.Application.Masters.UserTypes.Commands.DeleteUserType;
using NexaSoft.Club.Application.Masters.UserTypes.Queries.GetUserType;
using NexaSoft.Club.Application.Masters.UserTypes.Queries.GetUserTypes;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.Masters.UserTypes;

[Route("api/[controller]")]
[ApiController]
public class UserTypeController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateUserType(CreateUserTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateUserTypeCommand(
             request.Name,
             request.Description,
             request.IsAdministrative,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateUserType(UpdateUserTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateUserTypeCommand(
           request.Id,
             request.Name,
             request.Description,
             request.IsAdministrative,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeleteUserType(DeleteUserTypeRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteUserTypeCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserTypes(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetUserTypesQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetUserType(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetUserTypeQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
