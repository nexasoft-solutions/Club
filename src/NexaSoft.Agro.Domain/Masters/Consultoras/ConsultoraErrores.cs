using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Masters.Consultoras;

public class ConsultoraErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Consultora.NoEncontrado",
        "No se encontro Consultora"
    );

    public static readonly Error Duplicado = new
    (
        "Consultora.Duplicado",
        "Ya existe una Consultora con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Consultora.ErrorSave",
        "Error al guardar Consultora"
    );

    public static readonly Error ErrorEdit = new
    (
        "Consultora.ErrorEdit",
        "Error al editar Consultora"
    );

    public static readonly Error ErrorDelete = new
    (
        "Consultora.ErrorDelete",
        "Error al eliminar Consultora"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Consultora.ErrorConsulta",
        "Error al listar Consultora"
    );
}
