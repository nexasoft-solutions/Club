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
