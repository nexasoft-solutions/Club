using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Masters.Users.Request;
using NexaSoft.Club.Application.Masters.Users.Commands.CreateUser;
using NexaSoft.Club.Application.Masters.Users.Commands.UpdateUser;
using NexaSoft.Club.Application.Masters.Users.Commands.DeleteUser;
using NexaSoft.Club.Application.Masters.Users.Queries.GetUser;
using NexaSoft.Club.Application.Masters.Users.Queries.GetUsers;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Api.Extensions;
using NexaSoft.Club.Application.Masters.Users.Commands.AssignRolesToUser;
using NexaSoft.Club.Api.Controllers.Masters.Users.Requests;
using NexaSoft.Club.Application.Masters.Users.Commands.RevokeRolesFromUser;
using NexaSoft.Club.Application.Masters.Users.Queries.GetUserRolesAndPermissions;
using NexaSoft.Club.Application.Masters.Users.Queries.GetUserRoles;
using NexaSoft.Club.Application.Masters.Users.Commands.ChangePassword;

namespace NexaSoft.Club.Api.Controllers.Masters.Users;

using Microsoft.AspNetCore.Authorization;
using NexaSoft.Club.Api.Attributes;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController(ISender _sender) : ControllerBase
{
    [HttpPost]
    [RequirePermission("User.CreateUser")]
    public async Task<IActionResult> CreateUser(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(
             request.LastName,
             request.FirstName,
             request.Email,
             request.Dni,
             request.Phone,
             request.UserTypeId,
             request.BirthDate,
             request.MemberId
        );
        var resultado = await _sender.Send(command, cancellationToken);
        return resultado.ToActionResult(this);
    }

    [HttpPut]
    [RequirePermission("User.UpdateUser")]
    public async Task<IActionResult> UpdateUser(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateUserCommand(
            request.Id,
            request.LastName,
            request.FirstName,
            request.Email,
            request.Dni,
            request.Phone,
            request.UserTypeId,
            request.BirthDate,
            request.MemberId,
            request.UserModification
        );
        var resultado = await _sender.Send(command, cancellationToken);
        return resultado.ToActionResult(this);
    }

    [HttpDelete]
    [RequirePermission("User.DeleteUser")]
    public async Task<IActionResult> DeleteUser(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand(
             request.Id,
             request.UserDelete
         );
        var resultado = await _sender.Send(command, cancellationToken);
        return resultado.ToActionResult(this);
    }

    [HttpGet]
    [RequirePermission("User.GetUser")]
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
    [RequirePermission("User.GetUser")]
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
    [RequirePermission("User.CreateUser")]
    public async Task<IActionResult> AssignRoles(AssignRolesRequest request, CancellationToken cancellationToken)
    {
        var command = new AssignRolesToUserCommand(request.UserId, request.RoleDefs);
        var result = await _sender.Send(command, cancellationToken);
        return result.ToActionResult(this);
    }

    [HttpPost("revoke-roles")]
    [RequirePermission("User.CreateUser")]
    public async Task<IActionResult> RevokeRoles(RevokeRolesRequest request, CancellationToken cancellationToken)
    {
        var command = new RevokeRolesFromUserCommand(request.UserId, request.RoleIds);
        var result = await _sender.Send(command, cancellationToken);
        return result.ToActionResult(this);
    }

    [HttpPost("change-password")]
    [RequirePermission("User.CreateUser")]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var command = new ChangePasswordCommand(request.UserId, request.NewPassword);
        var result = await _sender.Send(command, cancellationToken);
        return result.ToActionResult(this);
    }

    
    [HttpGet("{userId:long}/permissions")]
    [RequirePermission("User.GetUser")]
    public async Task<IActionResult> GetUserPermissions(long userId, CancellationToken cancellationToken)
    {
        var query = new GetUserRolesAndPermissionsQuery(userId);
        var result = await _sender.Send(query, cancellationToken);
        return result.ToActionResult(this);
    }
    
    [HttpGet("roles")]
    [RequirePermission("User.GetUser")]
    public async Task<IActionResult> GetUserRoles([FromQuery]long userId, CancellationToken cancellationToken)
    {
        var query = new GetUserRolesQuery(userId);
        var result = await _sender.Send(query, cancellationToken);
        return result.ToActionResult(this);
    }
}