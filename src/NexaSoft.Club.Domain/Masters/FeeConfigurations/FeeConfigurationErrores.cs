using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.FeeConfigurations;

public class FeeConfigurationErrores
{
    public static readonly Error NoEncontrado = new
    (
        "FeeConfiguration.NoEncontrado",
        "No se encontro FeeConfiguration"
    );

    public static readonly Error Duplicado = new
    (
        "FeeConfiguration.Duplicado",
        "Ya existe una FeeConfiguration con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "FeeConfiguration.ErrorSave",
        "Error al guardar FeeConfiguration"
    );

    public static readonly Error ErrorEdit = new
    (
        "FeeConfiguration.ErrorEdit",
        "Error al editar FeeConfiguration"
    );

    public static readonly Error ErrorDelete = new
    (
        "FeeConfiguration.ErrorDelete",
        "Error al eliminar FeeConfiguration"
    );

    public static readonly Error ErrorConsulta = new
    (
        "FeeConfiguration.ErrorConsulta",
        "Error al listar FeeConfiguration"
    );
}
