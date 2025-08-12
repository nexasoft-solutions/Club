using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Estructuras;

public class EstructuraErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Estructura.NoEncontrado",
        "No se encontro Estructura"
    );

    public static readonly Error Duplicado = new
    (
        "Estructura.Duplicado",
        "Ya existe una Estructura con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Estructura.ErrorSave",
        "Error al guardar Estructura"
    );

    public static readonly Error ErrorEdit = new
    (
        "Estructura.ErrorEdit",
        "Error al editar Estructura"
    );

    public static readonly Error ErrorDelete = new
    (
        "Estructura.ErrorDelete",
        "Error al eliminar Estructura"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Estructura.ErrorConsulta",
        "Error al listar Estructura"
    );
}
