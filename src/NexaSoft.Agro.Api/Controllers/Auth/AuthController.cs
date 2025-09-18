using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Agro.Api.Controllers.Auth.request;
using NexaSoft.Agro.Api.Controllers.Auth.Request;
using NexaSoft.Agro.Api.Extensions;
using NexaSoft.Agro.Application.Masters.Auths.Queries.AuthToken;
using NexaSoft.Agro.Application.Masters.Auths.Queries.ChangeActiveRole;
using NexaSoft.Agro.Application.Masters.Auths.Queries.RefreshTokens;

namespace NexaSoft.Agro.Api.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ISender _sender) : ControllerBase
    {


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AuthLoginRequest request, CancellationToken cancellationToken)
        {
            var query = new AuthQuery(
                request.UserName,
                request.Password

            );
            var resultado = await _sender.Send(query, cancellationToken);

            return resultado.ToActionResult(this);
        }


        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var query = new RefreshTokenQuery(request.RefreshToken);
            var resultado = await _sender.Send(query, cancellationToken);

            return resultado.ToActionResult(this);
        }

        [HttpPost("change-role")]
        public async Task<IActionResult> ChangeActiveRole(ChangeActiveRoleRequest request, CancellationToken cancellationToken)
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!long.TryParse(userIdStr, out var userId))
                return Unauthorized("Usuario inválido.");

            var query = new ChangeActiveRoleQuery(userId, request.NewRoleId);
            var resultado = await _sender.Send(query, cancellationToken);

            return resultado.ToActionResult(this);
        }
    }
}
