using MediatR;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Controllers.Masters.Menus.Request;
using NexaSoft.Club.Api.Extensions;
using NexaSoft.Club.Application.Masters.MenuItems.Commands.AddRoleMenu;
using NexaSoft.Club.Application.Masters.MenuItems.Commands.CreateMenu;
using NexaSoft.Club.Application.Masters.MenuItems.Commands.DeleteMenu;
using NexaSoft.Club.Application.Masters.MenuItems.Commands.UpdateMenu;
using NexaSoft.Club.Application.Masters.MenuItems.Queries.GetMenu;
using NexaSoft.Club.Application.Masters.MenuItems.Queries.GetMenuByUser;
using NexaSoft.Club.Application.Masters.MenuItems.Queries.GetMenuByUserAndRole;
using NexaSoft.Club.Application.Masters.MenuItems.Queries.GetMenuRoles;
using NexaSoft.Club.Application.Masters.MenuItems.Queries.GetMenus;
using NexaSoft.Club.Application.Masters.MenuItems.Queries.GetTreeMenus;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Api.Controllers.Masters.Menus
{
    using Microsoft.AspNetCore.Authorization;
    using NexaSoft.Club.Api.Attributes;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MenuController(ISender _sender) : ControllerBase
    {
        [HttpPost]
        [RequirePermission("Menu.CreateMenu")]
        public async Task<IActionResult> CreateMenu(CreateMenuRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateMenuCommand(
                 request.Label,
                 request.Icon,
                 request.Route,
                 request.ParentId,
                 request.CreatedBy
            );
            var resultado = await _sender.Send(command, cancellationToken);
            return resultado.ToActionResult(this);
        }

        [HttpPut]
        [RequirePermission("Menu.UpdateMenu")]
        public async Task<IActionResult> UpdateMenu(UpdateMenuRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateMenuCommand(
                 request.Id,
                 request.Label,
                 request.Icon,
                 request.Route,
                 request.UpdatedBy
            );
            var resultado = await _sender.Send(command, cancellationToken);
            return resultado.ToActionResult(this);
        }

        [HttpDelete]
        [RequirePermission("Menu.DeleteMenu")]
        public async Task<IActionResult> DeleteMenu(DeleteMenuRequest request, CancellationToken cancellationToken)
        {
            var query = new DeleteMenuCommand(request.Id, request.DeletedBy);
            var resultado = await _sender.Send(query, cancellationToken);
            return resultado.ToActionResult(this);
        }

        [HttpGet]
        [RequirePermission("Menu.GetMenu")]
        public async Task<IActionResult> GetMenus(
            [FromQuery] BaseSpecParams specParams,
            CancellationToken cancellationToken
        )
        {
            var query = new GetMenusQuery(specParams);
            var resultado = await _sender.Send(query, cancellationToken);
            return resultado.ToActionResult(this);
        }

        [HttpGet("{id:long}")]
        [RequirePermission("Menu.GetMenu")]
        public async Task<IActionResult> GetMenu(long id, CancellationToken cancellationToken)
        {
            var query = new GetMenuQuery(id);
            var resultado = await _sender.Send(query, cancellationToken);

            return resultado.ToActionResult(this);
        }

        //[HttpGet("UserId/{id:long}/RoleId/{idRole:long}")]
        [HttpGet("menu-user-roles")]
        [GeneratePermission]
        [RequirePermission("Menu.GetMenu")]
        public async Task<IActionResult> GetMenuByUser(long userId, long roleId, CancellationToken cancellationToken)
        {

            var query = new GetMenuByUserQuery(
                userId,
                roleId);
            var resultado = await _sender.Send(query, cancellationToken);

            return resultado.ToActionResult(this);
        }

        [HttpGet("menu-roles")]
        [GeneratePermission]
        [RequirePermission("Menu.GetMenu")]
        public async Task<IActionResult> GetMenuRoles(long menuId, CancellationToken cancellationToken)
        {
            var query = new GetMenuRolesQuery(menuId);
            var resultado = await _sender.Send(query, cancellationToken);

            return resultado.ToActionResult(this);
        }

        [HttpGet("menu-user-role")]
        [GeneratePermission]
        [RequirePermission("Menu.GetMenu")]
        public async Task<IActionResult> GetMenuByUserAndRole(long userId, long roleId, CancellationToken cancellationToken)
        {
            var query = new GetMenuByUserAndRoleQuery(
                userId,
                roleId);
            var resultado = await _sender.Send(query, cancellationToken);

            return resultado.ToActionResult(this);
        }

        [HttpPost("assign-roles")]
        [GeneratePermission]
        [RequirePermission("Menu.CreateMenu")]
        public async Task<IActionResult> AssignRolesToMenu(AddRoleMenuRequest request, CancellationToken cancellationToken)
        {
            var command = new AddRoleMenuCommand(request.MenuId, request.RoleIds);
            var result = await _sender.Send(command, cancellationToken);
            return result.ToActionResult(this);
        }

        [HttpGet("tree")]
        [GeneratePermission]
        [RequirePermission("Menu.GetMenu")]
        public async Task<IActionResult> GetTreeMenus(CancellationToken cancellationToken)
        {
            var query = new GetTreeMenusQuery();
            var resultado = await _sender.Send(query, cancellationToken);

            return resultado.ToActionResult(this);
        }
    }
}
