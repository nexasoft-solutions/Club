using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Masters.Permissions.Request;
using NexaSoft.Club.Api.Extensions;
using NexaSoft.Club.Application.Features.Admins.SincronizarPermisos;
using NexaSoft.Club.Application.Masters.Permissions.Commands.CreatePermision;
using NexaSoft.Club.Application.Masters.Permissions.Commands.CreatePermission;
using NexaSoft.Club.Application.Masters.Permissions.Commands.DeletePermission;
using NexaSoft.Club.Application.Masters.Permissions.Queries.GetPermissions;
using NexaSoft.Club.Application.Masters.Permissions.Queries.GetRolesPermissions;

namespace NexaSoft.Club.Api.Controllers.Masters.Permissions
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController(ISender _sender) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreatePermission(CreatePermissionRequest request, CancellationToken cancellationToken)
        {
            var command = new CreatePermissionCommand(
                 request.Name,
                 request.Description,
                 request.ReferenciaControl,
                 request.UsuarioCreacion
            );
            var resultado = await _sender.Send(command, cancellationToken);

            return resultado.ToActionResult(this);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePermission(UpdatePermissionRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdatePermissionCommand(
                 request.Id,
                 request.Name,
                 request.Description,
                 request.ReferenciaControl,
                 request.UsuarioModificacion
            );
            var resultado = await _sender.Send(command, cancellationToken);

            return resultado.ToActionResult(this);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeletePermission(long id, CancellationToken cancellationToken)
        {
            var command = new DeletePermissionCommand(
                 id
             );
            var resultado = await _sender.Send(command, cancellationToken);

            return resultado.ToActionResult(this);
        }

        [HttpPost("sync")]
        public async Task<IActionResult> SyncPermissions(CancellationToken cancellationToken)
        {
            var command = new SincronizarPermisosCommand();
            var resultado = await _sender.Send(command, cancellationToken);
            return resultado.ToActionResult(this);
        }

        [HttpGet]
        public async Task<IActionResult> GetPermissions(
            CancellationToken cancellationToken
        )
        {
            var query = new GetPermissionsQuery();
            var resultado = await _sender.Send(query, cancellationToken);

            return resultado.ToActionResult(this);
        }
        
        [HttpGet("roles-permissions")]
        public async Task<IActionResult> GetRolPermissions(            
            CancellationToken cancellationToken
        )
        {
            var query = new GetRolesPermissionsQuery();
            var resultado = await _sender.Send(query, cancellationToken);

            return resultado.ToActionResult(this);
        }

    }
}
