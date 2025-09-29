using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Masters.SystemUsers.Request;
using NexaSoft.Club.Application.Masters.SystemUsers.Commands.CreateSystemUser;
using NexaSoft.Club.Application.Masters.SystemUsers.Commands.UpdateSystemUser;
using NexaSoft.Club.Application.Masters.SystemUsers.Commands.DeleteSystemUser;
using NexaSoft.Club.Application.Masters.SystemUsers.Queries.GetSystemUser;
using NexaSoft.Club.Application.Masters.SystemUsers.Queries.GetSystemUsers;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;

namespace NexaSoft.Club.Api.Controllers.Masters.SystemUsers;

[Route("api/[controller]")]
[ApiController]
public class SystemUserController(ISender _sender) : ControllerBase
{

    [HttpPost]
   public async Task<IActionResult> CreateSystemUser(CreateSystemUserRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateSystemUserCommand(
             request.Username,
             request.Email,
             request.FirstName,
             request.LastName,
             request.Role,
             request.IsActive,
    request.CreatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
   public async Task<IActionResult> UpdateSystemUser(UpdateSystemUserRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateSystemUserCommand(
           request.Id,
             request.Username,
             request.Email,
             request.FirstName,
             request.LastName,
             request.Role,
             request.IsActive,
             request.UpdatedBy
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
   public async Task<IActionResult> DeleteSystemUser(DeleteSystemUserRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteSystemUserCommand(
             request.Id,
             request.DeletedBy
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetSystemUsers(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetSystemUsersQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


   [HttpGet("{id:long}")]
   public async Task<IActionResult> GetSystemUser(
       long id,
       CancellationToken cancellationToken
    )
    {
        var query = new GetSystemUserQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

}
