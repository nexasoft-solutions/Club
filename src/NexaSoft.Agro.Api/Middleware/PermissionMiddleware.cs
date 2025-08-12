using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using NexaSoft.Agro.Api.Attributes;
using NexaSoft.Agro.Api.Controllers.Auth.Request;

namespace NexaSoft.Agro.Api.Middleware;

public class PermissionMiddleware(RequestDelegate _next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            var endpoint = context.GetEndpoint();

            // 🔓 Omitir validación si el endpoint tiene [AllowAnonymous]
            if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() is not null)
            {
                await _next(context);
                return;
            }

            // 🔍 Buscar atributos personalizados
            var permissionAttribute = endpoint?.Metadata.GetMetadata<RequirePermissionAttribute>();
            var roleAttribute = endpoint?.Metadata.GetMetadata<RequireRoleAttribute>();

            // ✅ Si no hay ninguno de los atributos, no validamos nada
            if (roleAttribute is null && permissionAttribute is null)
            {
                await _next(context);
                return;
            }

            var user = context.User;

            // ❌ Usuario no autenticado
            if (!user.Identity?.IsAuthenticated ?? true)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Usuario no autenticado.");
                return;
            }

            // ✅ Validar rol si se requiere
            /*if (roleAttribute is not null)
            {
                var userRoles = user.Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                if (!userRoles.Contains(roleAttribute.Role))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Rol no autorizado.");
                    return;
                }
            }*/

            /*foreach (var claim in user.Claims)
            {
                Console.WriteLine($"Claim recibido: Type = '{claim.Type}', Value = '{claim.Value}'");
            }*/

            if (roleAttribute is not null)
            {
                var rolesClaim = user.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.Role || c.Type == "roles")?.Value;

                if (rolesClaim is null)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("No se encontraron roles en el token.");
                    return;
                }

                List<RoleRequest>? roles;



                try
                {
                    roles = JsonSerializer.Deserialize<List<RoleRequest>>(rolesClaim);
                }
                catch
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Error al deserializar los roles.");
                    return;
                }

                foreach (var rol in roles!)
                {
                    Console.WriteLine($"Rol  = '{rol.id}', Value = '{rol.name}'");
                }

                if (!roles!.Any(r => string.Equals(r.name, roleAttribute.Role, StringComparison.OrdinalIgnoreCase)))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Rol no autorizado.");
                    return;
                }
            }


            // ✅ Validar permiso si se requiere
            if (permissionAttribute is not null)
            {
                var userPermissions = user.Claims
                    .Where(c => c.Type == "permission")
                    .Select(c => c.Value)
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                if (!userPermissions.Contains(permissionAttribute.Permission))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Permiso denegado.");
                    return;
                }
            }

            // ✅ Continuar con el pipeline si todo es válido
            await _next(context);
        }
        catch (Exception ex)
        {
            // 🛑 Captura de errores inesperados
            Console.WriteLine($"[Middleware Error] {ex.Message}");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("Error interno en middleware de permisos.");
        }
    }
}
