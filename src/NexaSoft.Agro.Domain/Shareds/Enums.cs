using System.ComponentModel;

namespace NexaSoft.Agro.Domain.Shareds;

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

}
