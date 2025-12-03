using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.Permissions;

namespace NexaSoft.Club.Application.Features.Admins.SincronizarPermisos;

public class SincronizarPermisosCommandHandler(
    IGenericRepository<Permission> _repository,
    IUnitOfWork _unitOfWork,
    IDateTimeProvider _dateTimeProvider,
    ILogger<SincronizarPermisosCommandHandler> _logger
) : ICommandHandler<SincronizarPermisosCommand, bool>
{
    public async Task<Result<bool>> Handle(SincronizarPermisosCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("🔄 Iniciando sincronización de permisos...");

        var permisosEncontrados = ObtenerPermisosDesdeControladores();
        var permisosExistentes = await _repository.ListAsync(cancellationToken);

        var nuevosPermisos = new List<Permission>();

        foreach (var permisoInfo in permisosEncontrados)
        {
            if (!permisosExistentes.Any(p => p.Name == permisoInfo.Name))
            {
                var nuevoPermiso = Permission.Create(
                    permisoInfo.Name,
                    permisoInfo.Description,
                    permisoInfo.Reference,
                    permisoInfo.Source,
                    permisoInfo.Action,
                    _dateTimeProvider.CurrentTime.ToUniversalTime(),
                    "system"
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
                    await _repository.AddAsync(permiso, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);

                _logger.LogInformation("✔ Se sincronizaron {Count} nuevos permisos.", nuevosPermisos.Count);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "❌ Error durante la sincronización de permisos.");
                return Result.Failure<bool>(new Error("Error.Sincronizacion", "Error al sincronizar permisos."));
            }
        }
        else
        {
            _logger.LogInformation("✓ No hay permisos nuevos para agregar.");
        }

        return Result.Success(true);
    }

    private List<PermisoInfo> ObtenerPermisosDesdeControladores()
    {
        var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
        var permisos = new List<PermisoInfo>();

        // Diccionario de traducción de módulos y acciones
        var diccionarioModulos = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "User", "Usuario" },
            { "Rol", "Rol" },
            { "Permission", "Permiso" },
            { "Menu", "Menú" },
            { "AccountingEntry", "Asiento Contable" },
            { "EntryItem", "Items de Asiento" },
            { "ExpenseVoucher", "Comprobante de Gasto" },
            { "FamilyMember", "Familiares del Miembro" },
            { "MemberFee", "Cuota de Socio" },
            { "Member", "Socio" },
            { "MemberVisit", "Visita de Socio" },
            { "Payment", "Pago" },
            { "Reservation", "Reserva" },
            { "AttendanceRecord", "Registro de Asistencia" },
            { "AttendanceStatusType", "Tipo de Estado de Asistencia" },
            { "BankAccountType", "Tipo de Cuenta Bancaria" },
            { "Bank", "Banco" },
            { "Company", "Empresa" },
            { "CompanyBankAccount", "Cuenta Bancaria de Empresa" },
            { "ConceptApplicationType", "Tipo de Aplicación de Concepto" },
            { "Reniec", "Reniec" },
            { "UserType", "Tipo de Usuario" },
            { "Status", "Estado" },
            { "Ubigeo", "Ubigeo" },
            { "SpaceType", "Tipo de Espacio" },
            { "Space", "Espacio" },
            { "SpaceRate", "Tarifas de Espacios" },
            { "SpacePhoto", "Fotos de Espacios" },
            { "SpaceAvailability", "Disponibilidad de Espacio" },
            { "SourceModule", "Módulo Fuente" },
            { "PaymentType", "Tipo de Pago" },
            { "Periodicity", "Periodicidad" },
            { "FeeConfiguration", "Configuración de Cuota" },
            { "MemberStatus", "Estado de Socio" },
            { "MemberType", "Tipo de Socio" },
            { "WorkSchedule", "Horario de Trabajo" },
            { "ConceptCalculationType", "Tipo de Cálculo de Concepto" },
            { "AccountingChart", "Plan Contable" },
            { "AccountType", "Tipo de Cuenta" },
            { "DocumentType", "Tipo de Documento" },
            { "TimeRequestType", "Tipo de Solicitud de Tiempo" },
            { "TaxRate", "Tasa de Impuesto" },
            { "TimeRequest", "Solicitud de Tiempo" },
            { "SpecialRate", "Tarifa Especial" },
            { "RateType", "Tipo de Tarifa" },
            { "PayrollFormula", "Fórmula de Planilla" },
            { "PayrollConfig", "Configuración de Planilla" },
            { "PayrollPeriod", "Periodo de Planilla" },
            { "PayrollPeriodStatus", "Estado de Periodo de Planilla" },
            { "PayrollConceptEmployeeType", "Tipo de Empleado de Concepto de Planilla" },
            { "PayrollConceptEmployee", "Concepto de Planilla por Empleado" },
            { "PayrollConcept", "Concepto de Planilla" },
            { "PayrollType", "Tipo de Planilla" },
            { "PayrollStatusType", "Tipo de Estado de Planilla" },
            { "Position", "Puesto" },
            { "PayrollConceptDepartment", "Departamento de Concepto de Planilla" },
            { "PayPeriodType", "Tipo de Periodo de Pago" },
            { "Expense", "Gasto" },
            { "EmployeeInfo", "Información de Empleado" },
            { "IncomeTaxScale", "Escala de Impuesto a la Renta" },
            { "PaymentMethodType", "Tipo de Método de Pago" },
            { "LegalParameter", "Parámetro Legal" },
            { "EmployeeType", "Tipo de Empleado" },
            { "EmploymentContract", "Contrato de Trabajo" },
            { "Department", "Departamento" },
            { "Currency", "Moneda" },
            { "CostCenter", "Centro de Costo" },
            { "ContractType", "Tipo de Contrato" },
            { "CostCenterType", "Tipo de Centro de Costo" },
            { "Auth", "Autenticación" },
            { "MemberAuth", "Autenticación de Socio" },
            { "ConceptTypePayroll", "Tipo de Concepto de Planilla"}
        };
        var diccionarioAcciones = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Get", "Consultar" },
            { "Create", "Crear" },
            { "Update", "Actualizar" },
            { "Delete", "Eliminar" },
            { "Action", "Acción" }
        };

        var controllers = assembly.GetTypes()
            .Where(type => typeof(ControllerBase).IsAssignableFrom(type) && !type.IsAbstract);

        foreach (var controller in controllers)
        {
            string module = controller.Name.Replace("Controller", "");
            string moduleDescriptivo = diccionarioModulos.TryGetValue(module, out var modEsp) ? modEsp : ConvertirModuloEnTexto(module);

            var methods = controller.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

            // Agrupar métodos por verbo HTTP
            var grouped = methods
                .Select(m => new {
                    Method = m,
                    HttpAttr = m.GetCustomAttributes().FirstOrDefault(a => a is HttpGetAttribute or HttpPostAttribute or HttpPutAttribute or HttpDeleteAttribute)
                })
                .Where(x => x.HttpAttr != null)
                .GroupBy(x => ObtenerAccionDesdeHttp(x.HttpAttr!));

            foreach (var group in grouped)
            {
                string verbo = group.Key;
                string actionEsp = diccionarioAcciones.TryGetValue(verbo, out var accEsp) ? accEsp : verbo;

                // El nombre del permiso debe ser {Entidad}.{Accion}{Entidad} (ej: Rol.GetRol), nunca plural
                var permisoNombre = $"{module}.{verbo}{module}";

                // Si el nombre termina en 's', quitar la 's' para asegurar singular
                if (permisoNombre.EndsWith("s", StringComparison.OrdinalIgnoreCase))
                {
                    permisoNombre = permisoNombre.Substring(0, permisoNombre.Length - 1);
                }

                string source = moduleDescriptivo;
                string action = actionEsp;
                string descripcion = $"{actionEsp} {moduleDescriptivo}";
                string referenciaControl = $"{moduleDescriptivo} / {actionEsp}";

                permisos.Add(new PermisoInfo(
                    permisoNombre,
                    descripcion,
                    referenciaControl,
                    source,
                    action
                ));
            }
        }

        return permisos
            .DistinctBy(x => x.Name)
            .OrderBy(x => x.Reference)
            .ToList();
    }

    private string ObtenerAccionDesdeHttp(Attribute attr)
    {
        return attr switch
        {
            HttpGetAttribute => "Get",
            HttpPostAttribute => "Create",
            HttpPutAttribute => "Update",
            HttpDeleteAttribute => "Delete",
            _ => "Action"
        };
    }

    private string ConvertirModuloEnTexto(string module)
    {
        return System.Text.RegularExpressions.Regex
            .Replace(module, "([a-z])([A-Z])", "$1 $2");
    }
}
