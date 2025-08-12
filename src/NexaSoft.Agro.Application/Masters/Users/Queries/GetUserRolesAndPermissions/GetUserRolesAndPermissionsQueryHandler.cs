using Microsoft.Extensions.Logging;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Masters.Users;

namespace NexaSoft.Agro.Application.Masters.Users.Queries.GetUserRolesAndPermissions;

public class GetUserRolesAndPermissionsQueryHandler(

    IUserRoleRepository _userRoleRepository,
    ILogger<GetUserRolesAndPermissionsQueryHandler> _logger)
    : IQueryHandler<GetUserRolesAndPermissionsQuery, List<UserRolesPermissionsResponse>>
{
    public async Task<Result<List<UserRolesPermissionsResponse>>> Handle(
        GetUserRolesAndPermissionsQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Obteniendo roles y permisos para el usuario {UserId}", query.UserId);

        try
        {
            _logger.LogInformation("Obteniendo roles y permisos para el usuario {UserId}", query.UserId);
            var response = await _userRoleRepository
                           .GetUserRolesPermissions(query.UserId, cancellationToken);

            Console.WriteLine("Response=>", response);
            return Result.Success(response);

        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener roles y permisos del usuario {UserId}", query.UserId);
            return Result.Failure<List<UserRolesPermissionsResponse>>(UserErrores.ErrorObtenerRolesPermisos);
        }
    }
}