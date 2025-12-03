using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Api.Attributes;
using NexaSoft.Club.Api.Controllers.Auth.Request;
using MediatR;
using NexaSoft.Club.Application.Masters.Roles.Queries.GetPermissionsByRole;

namespace NexaSoft.Club.Api.Middleware;

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
            var requiredPermission = NormalizarPermiso(permissionAttribute.Permission);

            // Extraer roleActive del claim
            var roleActiveClaim = user.Claims.FirstOrDefault(c => c.Type == "roleActive")?.Value;
            if (string.IsNullOrWhiteSpace(roleActiveClaim) || !long.TryParse(roleActiveClaim, out var roleId))
            {
                await WriteProblemDetailsAsync(context, StatusCodes.Status403Forbidden,
                    "Rol activo faltante",
                    "No se encontró el rol activo en el token.");
                return;
            }

            // Crear scope y resolver ISender
            using var scope = context.RequestServices.CreateScope();
            var sender = scope.ServiceProvider.GetRequiredService<ISender>();
            var query = new GetPermissionsByRolQuery(roleId);
            var result = await sender.Send(query, context.RequestAborted);
            var userPermissions = result.IsSuccess ? result.Value : new List<string>();

            // Comparación normalizada y case-insensitive
            if (!userPermissions.Any(p =>
                string.Equals(NormalizarPermiso(p), requiredPermission, StringComparison.OrdinalIgnoreCase)))
            {
                await WriteProblemDetailsAsync(context, StatusCodes.Status403Forbidden,
                    "Permiso denegado",
                    $"Se requiere el permiso: {requiredPermission}.");
                return;
            }
        }

        // 🎯 Todo válido → Continuar
        await _next(context);
    }

    private string NormalizarPermiso(string permiso)
    {
        if (string.IsNullOrEmpty(permiso))
            return permiso;

        // Separar por puntos: {Entidad}.{Accion}{Entidad}
        var parts = permiso.Split('.');
        if (parts.Length != 2)
            return permiso;

        var entidad = parts[0];
        var accionEntidad = parts[1];

        // Normalizar "Role" a "Rol"
        if (accionEntidad.EndsWith("Role", StringComparison.OrdinalIgnoreCase))
        {
            accionEntidad = accionEntidad.Substring(0, accionEntidad.Length - 4) + "Rol";
        }
        // O si termina en "Rol" mantenerlo
        else if (!accionEntidad.EndsWith("Rol", StringComparison.OrdinalIgnoreCase) &&
                 accionEntidad.EndsWith("s", StringComparison.OrdinalIgnoreCase))
        {
            // Quitar 's' final para singular
            accionEntidad = accionEntidad.Substring(0, accionEntidad.Length - 1);
        }

        // También normalizar la entidad si es "Role" a "Rol"
        if (string.Equals(entidad, "Role", StringComparison.OrdinalIgnoreCase))
        {
            entidad = "Rol";
        }

        return $"{entidad}.{accionEntidad}";
    }

    private static async Task WriteProblemDetailsAsync(HttpContext context, int statusCode, string title, string detail)
    {
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}