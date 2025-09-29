using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.Ubigeos;

public class UbigeoErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Ubigeo.NoEncontrado",
        "No se encontro Ubigeo"
    );

    public static readonly Error Duplicado = new
    (
        "Ubigeo.Duplicado",
        "Ya existe una Ubigeo con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Ubigeo.ErrorSave",
        "Error al guardar Ubigeo"
    );

    public static readonly Error ErrorEdit = new
    (
        "Ubigeo.ErrorEdit",
        "Error al editar Ubigeo"
    );

    public static readonly Error ErrorDelete = new
    (
        "Ubigeo.ErrorDelete",
        "Error al eliminar Ubigeo"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Ubigeo.ErrorConsulta",
        "Error al listar Ubigeo"
    );
}
