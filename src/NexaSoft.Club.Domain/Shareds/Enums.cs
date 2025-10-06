using System.ComponentModel;

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

    public enum EstadosEventosEnum
    {
        [Description("Programado")]
        Programado = 1,

        [Description("Presentado")]
        Presentado = 2,

        [Description("Vencido")]
        Vencido = 3,

        [Description("Reprogramado")]
        Reprogramado = 4,

        [Description("Cancelado")]
        Cancelado = 5
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

    public enum TipoEntidadArchivo
    {
        EstudioAmbiental = 1,
        Capitulo = 2,
        SubCapitulo = 3,
        Estructura = 4
    }


    public enum PeriodicidadEnum
    {
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

    public enum VisitTypeEnum
    {
        [Description("Entrada Normal")]
        Normal = 1,

        [Description("Entrada con Visita")]
        ConVisita = 2,

    }

}
