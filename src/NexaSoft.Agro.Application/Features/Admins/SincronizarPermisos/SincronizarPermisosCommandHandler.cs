using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Abstractions.Time;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Permissions;

namespace NexaSoft.Agro.Application.Features.Admins.SincronizarPermisos;

public class SincronizarPermisosCommandHandler(
    IGenericRepository<Permission> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<SincronizarPermisosCommandHandler> _logger
) : ICommandHandler<SincronizarPermisosCommand, bool>
{
    public async Task<Result<bool>> Handle(SincronizarPermisosCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando sincronización de permisos...");

        var permisosEncontrados = ObtenerPermisosDesdeControladores();
        var permisosExistentes = await _repository.ListAsync(cancellationToken);

        var nuevosPermisos = new List<Permission>();

        foreach (var permisoInfo in permisosEncontrados)
        {
            if (!permisosExistentes.Any(p => p.Name == permisoInfo.Nombre))
            {
                var nuevoPermiso = Permission.Create(
                    permisoInfo.Nombre,
                    permisoInfo.Descripcion,
                    permisoInfo.Controlador,
                    _dateTimeProvider.CurrentTime.ToUniversalTime(),
                    string.Empty
                );
                nuevosPermisos.Add(nuevoPermiso);
            }
        }

        if (nuevosPermisos.Any())
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);
                foreach (var permiso in nuevosPermisos)
                {
                    await _repository.AddAsync(permiso, cancellationToken);
                }
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
                _logger.LogInformation("Se sincronizaron {Count} permisos.", nuevosPermisos.Count);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Error durante la sincronización de permisos.");
                return Result.Failure<bool>(new Error("Error.Sincronizacion", "Error al sincronizar permisos."));
            }
        }
        else
        {
            _logger.LogInformation("No se encontraron nuevos permisos para agregar.");
        }

        return Result.Success(true);
    }

    private List<PermisoInfo> ObtenerPermisosDesdeControladores()
    {
        var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
        var permisos = new List<PermisoInfo>();

        var controllers = assembly.GetTypes()
            .Where(type => typeof(ControllerBase).IsAssignableFrom(type) && !type.IsAbstract);

        foreach (var controller in controllers)
        {
            var controllerName = controller.Name.Replace("Controller", "");

            var methods = controller.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (var method in methods)
            {
                var hasHttpAttribute = method.GetCustomAttributes()
                    .Any(attr => attr is HttpGetAttribute || attr is HttpPostAttribute ||
                                 attr is HttpPutAttribute || attr is HttpDeleteAttribute);

                if (hasHttpAttribute)
                {
                    var nombrePermiso = $"{controllerName}.{method.Name}";
                    permisos.Add(new PermisoInfo
                    (
                        nombrePermiso,
                        $"Permiso generado para el método {method.Name} del controlador {controllerName}",
                        controllerName
                    ));
                }
            }
        }

        return permisos.DistinctBy(p => p.Nombre).ToList();
    }
}
