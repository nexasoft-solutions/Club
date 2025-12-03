using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Attributes;
using NexaSoft.Club.Api.Controllers.Masters.Roles.Request;
using NexaSoft.Club.Api.Extensions;
using NexaSoft.Club.Application.Masters.Permissions.Commands.AssignPermissionToRole;
using NexaSoft.Club.Application.Masters.Permissions.Commands.RevokePermissionFromRole;
using NexaSoft.Club.Application.Masters.Roles.Commands.CreateRole;
using NexaSoft.Club.Application.Masters.Roles.Commands.DeleteRole;
using NexaSoft.Club.Application.Masters.Roles.Queries.GetRoles;
using NexaSoft.Club.Application.Masters.Roles.Queries.GetRolesPermissions;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Api.Controllers.Masters.Roles
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolController(ISender _sender) : ControllerBase
    {
        [HttpPost]
        [GeneratePermission]
        [RequirePermission("Rol.CreateRol")]
        public async Task<IActionResult> CreateRol(CreateRoleRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateRoleCommand(
                 request.Name,
                 request.Description,
                 request.CreatedBy
            );
            var resultado = await _sender.Send(command, cancellationToken);

            return resultado.ToActionResult(this);
        }

        [HttpPut]
        [GeneratePermission]
        [RequirePermission("Rol.UpdateRol")]
        public async Task<IActionResult> UpdateRole(UpdateRoleRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateRoleCommand(
                 request.Id,
                 request.Name,
                 request.Description,
                 request.UpdatedBy
            );
            var resultado = await _sender.Send(command, cancellationToken);

            return resultado.ToActionResult(this);
        }

        [HttpDelete("{id:long}")]
        [GeneratePermission]
        [RequirePermission("Rol.DeleteRol")]
        public async Task<IActionResult> DeleteRole(long id, CancellationToken cancellationToken)
        {
            var command = new DeleteRoleCommand(
                 id
             );
            var resultado = await _sender.Send(command, cancellationToken);

            return resultado.ToActionResult(this);
        }

        [HttpGet]
        [GeneratePermission]
        [RequirePermission("Rol.GetRol")]
        public async Task<IActionResult> GetRoles(          
            [FromQuery] BaseSpecParams specParams,
            CancellationToken cancellationToken
        )
        {
            var query = new GetRolesQuery(specParams);
            var resultado = await _sender.Send(query, cancellationToken);

            return resultado.ToActionResult(this);
        }

        [HttpGet("permissions")]
        [GeneratePermission]
        [RequirePermission("Rol.GetRol")]
        public async Task<IActionResult> GetRolesPermissions([FromQuery] long roleId, CancellationToken cancellationToken)
        {
            var query = new GetRolesPermissionsQuery(roleId);
            var resultado = await _sender.Send(query, cancellationToken);

            return resultado.ToActionResult(this);
        }

        [HttpPost("assign-permissions")]
        [GeneratePermission]
        [RequirePermission("Rol.CreateRol")]
        public async Task<IActionResult> AssignPermissions(AssignPermissionsRequest request, CancellationToken cancellationToken)
        {
            var command = new AssignPermissionToRoleCommand(
                request.RoleId,
                request.PermissionIds
            );

            var result = await _sender.Send(command, cancellationToken);

            return result.ToActionResult(this);
        }

        [HttpPost("revoke-permissions")]
        [GeneratePermission]
        [RequirePermission("Rol.CreateRol")]
        public async Task<IActionResult> RevokePermissions(AssignPermissionsRequest request, CancellationToken cancellationToken)
        {
            var command = new RevokePermissionFromRoleCommand(request.RoleId, request.PermissionIds);
            var result = await _sender.Send(command, cancellationToken);
            return result.ToActionResult(this);
        }
    }
}
