using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Agro.Api.Controllers.Masters.Menus.Request;
using NexaSoft.Agro.Api.Extensions;
using NexaSoft.Agro.Application.Masters.MenuItems.Commands.AddRoleMenu;
using NexaSoft.Agro.Application.Masters.MenuItems.Commands.CreateMenu;
using NexaSoft.Agro.Application.Masters.MenuItems.Commands.DeleteMenu;
using NexaSoft.Agro.Application.Masters.MenuItems.Commands.UpdateMenu;
using NexaSoft.Agro.Application.Masters.MenuItems.Queries.GetMenuByUser;
using NexaSoft.Agro.Application.Masters.MenuItems.Queries.GetMenus;

namespace NexaSoft.Agro.Api.Controllers.Masters.Menus
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController(ISender _sender) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateMenu(CreateMenuRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateMenuCommand(
                 request.Label,
                 request.Icon,
                 request.Route,
                 request.ParentId,
                 request.UsuarioCreacion
            );
            var resultado = await _sender.Send(command, cancellationToken);

            return resultado.ToActionResult(this);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMenu(UpdateMenuRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateMenuCommand(
                 request.Id,
                 request.Label,
                 request.Icon,
                 request.Route,
                 request.UsuarioModificacion
            );
            var resultado = await _sender.Send(command, cancellationToken);

            return resultado.ToActionResult(this);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMenu(DeleteMenuRequest request, CancellationToken cancellationToken)
        {

            var query = new DeleteMenuCommand(request.Id,request.UsuarioEliminacion);
            var resultado = await _sender.Send(query, cancellationToken);

            return resultado.ToActionResult(this);
        }


        [HttpGet]
        public async Task<IActionResult> GetMenus(
         CancellationToken cancellationToken
        )
        {
            var query = new GetMenusQuery();
            var resultado = await _sender.Send(query, cancellationToken);

            return resultado.ToActionResult(this);
        }

        //[HttpGet("UserId/{id:long}/RoleId/{idRole:long}")]
        [HttpGet("menu-user-roles")]
        public async Task<IActionResult> GetMenuByUser(long userId, long roleId, CancellationToken cancellationToken)
        {

            var query = new GetMenuByUserQuery(
                userId,
                roleId);
            var resultado = await _sender.Send(query, cancellationToken);

            return resultado.ToActionResult(this);
        }

        [HttpPost("assign-roles")]
        public async Task<IActionResult> AssignRolesToMenu(AddRoleMenuRequest request, CancellationToken cancellationToken)
        {
            var command = new AddRoleMenuCommand(request.MenuId, request.RoleIds);
            var result = await _sender.Send(command, cancellationToken);
            return result.ToActionResult(this);
        }
    }
}
