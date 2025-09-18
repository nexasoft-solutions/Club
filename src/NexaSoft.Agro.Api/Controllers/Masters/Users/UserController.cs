using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Agro.Api.Controllers.Masters.Users.Request;
using NexaSoft.Agro.Application.Masters.Users.Commands.CreateUser;
using NexaSoft.Agro.Application.Masters.Users.Commands.UpdateUser;
using NexaSoft.Agro.Application.Masters.Users.Commands.DeleteUser;
using NexaSoft.Agro.Application.Masters.Users.Queries.GetUser;
using NexaSoft.Agro.Application.Masters.Users.Queries.GetUsers;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Api.Extensions;
using NexaSoft.Agro.Application.Masters.Users.Commands.AssignRolesToUser;
using NexaSoft.Agro.Api.Controllers.Masters.Users.Requests;
using NexaSoft.Agro.Application.Masters.Users.Commands.RevokeRolesFromUser;
using NexaSoft.Agro.Application.Masters.Users.Queries.GetUserRolesAndPermissions;

namespace NexaSoft.Agro.Api.Controllers.Masters.Users;

[Route("api/[controller]")]
[ApiController]
public class UserController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(
             request.UserApellidos,
             request.UserNombres,
             request.Password,
             request.Email,
             request.UserDni,
             request.UserTelefono

        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateUserCommand(
           request.Id,
             request.UserApellidos,
             request.UserNombres,
             request.Password,
             request.Email,
             request.UserDni,
             request.UserTelefono,
             request.UsuarioModificacion
        );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand(
             request.Id,
             request.UsuarioEliminacion
         );
        var resultado = await _sender.Send(command, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers(
       [FromQuery] BaseSpecParams specParams,
       CancellationToken cancellationToken
    )
    {
        var query = new GetUsersQuery(specParams);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetUser(
        long id,
        CancellationToken cancellationToken
     )
    {
        var query = new GetUserQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);

        return resultado.ToActionResult(this);
    }

    [HttpPost("assign-roles")]
    public async Task<IActionResult> AssignRoles(AssignRolesRequest request, CancellationToken cancellationToken)
    {
        var command = new AssignRolesToUserCommand(request.UserId, request.RoleDefs);
        var result = await _sender.Send(command, cancellationToken);
        return result.ToActionResult(this);
    }

    [HttpPost("revoke-roles")]
    public async Task<IActionResult> RevokeRoles(RevokeRolesRequest request, CancellationToken cancellationToken)
    {
        var command = new RevokeRolesFromUserCommand(request.UserId, request.RoleIds);
        var result = await _sender.Send(command, cancellationToken);
        return result.ToActionResult(this);
    }

    
    [HttpGet("{userId:long}/permissions")]
    public async Task<IActionResult> GetUserPermissions(long userId, CancellationToken cancellationToken)
    {
        var query = new GetUserRolesAndPermissionsQuery(userId);
        var result = await _sender.Send(query, cancellationToken);
        return result.ToActionResult(this);
    }

}
