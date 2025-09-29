using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Masters.Roles.Request;
using NexaSoft.Club.Api.Extensions;
using NexaSoft.Club.Application.Masters.Permissions.Commands.AssignPermissionToRole;
using NexaSoft.Club.Application.Masters.Permissions.Commands.RevokePermissionFromRole;
using NexaSoft.Club.Application.Masters.Roles.Commands.CreateRole;
using NexaSoft.Club.Application.Masters.Roles.Commands.DeleteRole;
using NexaSoft.Club.Application.Masters.Roles.Queries.GetRoles;

namespace NexaSoft.Club.Api.Controllers.Masters.Roles
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController(ISender _sender) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateRol(CreateRoleRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateRoleCommand(
                 request.Name,
                 request.Description,
                 request.UsuarioCreacion
            );
            var resultado = await _sender.Send(command, cancellationToken);

            return resultado.ToActionResult(this);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole(UpdateRoleRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateRoleCommand(
                 request.Id,
                 request.Name,
                 request.Description,
                 request.UsuarioModificacion
            );
            var resultado = await _sender.Send(command, cancellationToken);

            return resultado.ToActionResult(this);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteRole(long id, CancellationToken cancellationToken)
        {
            var command = new DeleteRoleCommand(
                 id
             );
            var resultado = await _sender.Send(command, cancellationToken);

            return resultado.ToActionResult(this);
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles(           
            CancellationToken cancellationToken
        )
        {
            var query = new GetRolesQuery();
            var resultado = await _sender.Send(query, cancellationToken);

            return resultado.ToActionResult(this);
        }

        [HttpPost("assign-permissions")]
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
        public async Task<IActionResult> RevokePermissions(AssignPermissionsRequest request, CancellationToken cancellationToken)
        {
            var command = new RevokePermissionFromRoleCommand(request.RoleId, request.PermissionIds);
            var result = await _sender.Send(command, cancellationToken);
            return result.ToActionResult(this);
        }
    }
}
