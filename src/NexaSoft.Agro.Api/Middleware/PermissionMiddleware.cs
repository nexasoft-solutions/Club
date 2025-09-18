using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Agro.Api.Attributes;
using NexaSoft.Agro.Api.Controllers.Auth.Request;

namespace NexaSoft.Agro.Api.Middleware;

public class PermissionMiddleware(RequestDelegate _next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();

        // 🔓 Si el endpoint permite acceso anónimo, continuar
        if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() is not null)
        {
            await _next(context);
            return;
        }

        // 🔍 Obtener los atributos personalizados
        var permissionAttribute = endpoint?.Metadata.GetMetadata<RequirePermissionAttribute>();
        var roleAttribute = endpoint?.Metadata.GetMetadata<RequireRoleAttribute>();

        // ✅ Si no se requiere ni permiso ni rol, continuar
        if (permissionAttribute is null && roleAttribute is null)
        {
            await _next(context);
            return;
        }

        var user = context.User;

        // ❌ Usuario no autenticado
        if (!user.Identity?.IsAuthenticated ?? true)
        {
            await WriteProblemDetailsAsync(context, StatusCodes.Status401Unauthorized, "No autenticado", "El usuario no está autenticado.");
            return;
        }

        // ✅ Validación de roles
        if (roleAttribute is not null)
        {
            var rolesClaim = user.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.Role || c.Type == "roles")?.Value;

            if (string.IsNullOrWhiteSpace(rolesClaim))
            {
                await WriteProblemDetailsAsync(context, StatusCodes.Status403Forbidden, "Roles faltantes", "No se encontraron roles en el token.");
                return;
            }

            List<RoleRequest>? roles;
            try
            {
                roles = JsonSerializer.Deserialize<List<RoleRequest>>(rolesClaim);
            }
            catch (Exception)
            {
                await WriteProblemDetailsAsync(context, StatusCodes.Status403Forbidden, "Error al procesar roles", "No se pudieron deserializar los roles del token.");
                return;
            }

            if (!roles!.Any(r => string.Equals(r.name, roleAttribute.Role, StringComparison.OrdinalIgnoreCase)))
            {
                await WriteProblemDetailsAsync(context, StatusCodes.Status403Forbidden, "Rol no autorizado", $"Se requiere el rol '{roleAttribute.Role}'.");
                return;
            }
        }

        // ✅ Validación de permisos
        if (permissionAttribute is not null)
        {
            var userPermissions = user.Claims
                .Where(c => c.Type == "permission")
                .Select(c => c.Value)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            if (!userPermissions.Contains(permissionAttribute.Permission))
            {
                await WriteProblemDetailsAsync(context, StatusCodes.Status403Forbidden, "Permiso denegado", $"Se requiere el permiso '{permissionAttribute.Permission}'.");
                return;
            }
        }

        // 🎯 Todo válido → Continuar
        await _next(context);
    }

    private static async Task WriteProblemDetailsAsync(HttpContext context, int statusCode, string title, string detail)
    {
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3" // Para 403 Forbidden
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}
