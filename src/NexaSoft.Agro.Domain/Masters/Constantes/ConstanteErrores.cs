using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Masters.Constantes;

public class ConstanteErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Constante.NoEncontrado",
        "No se encontro Constante"
    );

    public static readonly Error Duplicado = new
    (
        "Constante.Duplicado",
        "Ya existe una Constante con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Constante.ErrorSave",
        "Error al guardar Constante"
    );

    public static readonly Error ErrorEdit = new
    (
        "Constante.ErrorEdit",
        "Error al editar Constante"
    );

    public static readonly Error ErrorDelete = new
    (
        "Constante.ErrorDelete",
        "Error al eliminar Constante"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Constante.ErrorConsulta",
        "Error al listar Constante"
    );
}
