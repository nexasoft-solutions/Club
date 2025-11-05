using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace NexaSoft.Club.Domain.Shareds;

public static class Enums
{
    public enum EstadosEnum
    {
        [Description("Activo")]
        Activo = 1,

        [Description("Eliminado")]
        Eliminado = 2
    }

    public enum StatusEnum
    {
        [Description("Iniciado")]
        Iniciado = 1,

        [Description("Completado")]
        Completado = 2,

        [Description("Fallido")]
        Fallido = 3,

        [Description("Activo")]
        Activo = 4,

        [Description("Futura")]
        Futura = 5,

        [Description("Pagado")]
        Pagado = 6,

        [Description("Parcialmente Pagado")]
        ParcialmentePagado = 7,

        [Description("Pendiente")]
        Pendiente = 8,
        [Description("Cancelado")]
        Cancelado = 9

    }

    public enum PayRollTypesEnum
    {
        [Description("Quincenal")]
        Quincenal = 1,

        [Description("Mensual")]
        Mensual = 2,


        [Description("Cts")]
        Cts = 3,

        [Description("Gratificación")]
        Gratificacion = 4
    }


    public enum UbigeosEnum
    {
        [Description("Departamento")]
        Departamento = 1,

        [Description("Provincia")]
        Provincia = 2,

        [Description("Distrito")]
        Distrito = 3
    }

    public enum TipoDocumentoEnum
    {
        [Description("Boleta")]
        Boleta = 1,

        [Description("Factura")]
        Factura = 2,

        [Description("Recibo por Honorarios")]
        ReciboPorHonorarios = 3,

        [Description("Ticket")]
        Ticket = 4,

        [Description("Otro")]
        Otro = 5
    }

    public enum PeriodicidadEnum
    {
        //resetear luego
        UicaVez = 1,
        Diario = 2,
        Semanal = 3,
        Quincenal = 4,
        Mensual = 5,
        Bimestral = 6,
        Trimestral = 7,
        Cuatrimestral = 8,
        Semestral = 9,
        Anual = 10,
        Bienal = 11,
        Plurianual = 12

    }

    public enum PaymentMethodEnum
    {
        [Description("Efectivo")]
        Efectivo = 1,

        [Description("Tarjeta de Crédito")]
        TarjetaDeCredito = 2,

        [Description("Tarjeta de Débito")]
        TarjetaDeDebito = 3,

        [Description("Transferencia Bancaria")]
        TransferenciaBancaria = 4,

        [Description("Yape")]
        Yape = 5,

        [Description("Plin")]
        Plin = 6,

        [Description("Otro")]
        Otro = 7
    }

    public enum TipoCuentaEnum
    {
        [Description("Caja/Bancos")]
        CajaBancos = 1,

        [Description("Clientes")]
        Clientes = 2,

        [Description("Proveedores")]
        Proveedores = 3,

        [Description("Capital")]
        Capital = 4,

        [Description("Ingresos")]
        Ingresos = 5,

        [Description("Costos")]
        Costos = 6,

        [Description("Gastos")]
        Gastos = 7,

        [Description("Otros Activos")]
        OtrosActivos = 8,

        [Description("Otros Pasivos")]
        OtrosPasivos = 9,

        [Description("Cuentas de Orden")]
        CuentasDeOrden = 10

    }

    public enum VisitTypeEnum
    {
        [Description("Entrada Normal")]
        Normal = 1,

        [Description("Entrada con Visita")]
        ConVisita = 2,

    }

    public enum SourceModuleEnum
    {
        [Description("Pagos")]
        Pagos = 1,

        /*[Description("Cobros")]
        Cobros = 2,

        [Description("Ventas")]
        Ventas = 3,

        [Description("Compras")]
        Compras = 4,

        [Description("Ajustes de Inventario")]
        AjustesDeInventario = 5,*/

        [Description("Reservaciones")]
        Reservaciones = 2,

        [Description("Membresías")]
        Membresias = 4,

        [Description("Planillas")]
        Planillas = 3,

        [Description("Ninguno")]
        Ninguno = 99
    }

    public enum TipoCuentaContable
    {
        Activo = 1,
        Pasivo = 2,
        Patrimonio = 3,
        Ingreso = 4,
        Gasto = 5
    }

    public enum CurrencyEnum
    {
        [Description("Sol Peruano")]
        SolPeruano = 1,

        [Description("Dólar Americano")]
        DolarEstadounidense = 2,


    }

}
