using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollPeriods;

public class PayrollPeriodErrores
{
    public static readonly Error NoEncontrado = new
    (
        "PayrollPeriod.NoEncontrado",
        "No se encontro PayrollPeriod"
    );

    public static readonly Error ErrorEdit = new
   (
       "PayrollPeriod.ErrorEdit",
       "Error al editar PayrollPeriod"
   );

    public static readonly Error ErrorDelete = new
    (
        "PayrollPeriod.ErrorDelete",
        "Error al eliminar PayrollPeriod"
    );

    public static readonly Error ErrorConsulta = new
    (
        "PayrollPeriod.ErrorConsulta",
        "Error al listar PayrollPeriod"
    );







    public static readonly Error ErrorSave = new(
        "PayrollPeriod.ErrorSave",
        "Error al guardar el período de planilla");

    public static readonly Error EmpleadoSinDatos = new(
        "PayrollPeriod.EmpleadoSinDatos",
        "No se encontraron datos para procesar la planilla del empleado");

    public static readonly Error Duplicado = new("PayrollPeriod.Duplicado", "Ya existe un período con el mismo nombre");
    public static readonly Error FechasInvalidas = new("PayrollPeriod.FechasInvalidas", "Las fechas del período son inválidas");
    public static readonly Error PeriodoSolapado = new("PayrollPeriod.PeriodoSolapado", "El período se solapa con otro período existente");

    public static readonly Error TipoPlanillaNoEncontrado = new("PayrollPeriod.TipoPlanillaNoEncontrado", "Tipo de planilla no encontrado");
    public static readonly Error ConceptosNoConfigurados = new("PayrollPeriod.ConceptosNoConfigurados", "No hay conceptos configurados para este tipo de planilla");
    public static readonly Error ParametroLegalNoConfigurado = new("PayrollPeriod.ParametroLegalNoConfigurado", "Parámetros legales no configurados correctamente");
}
