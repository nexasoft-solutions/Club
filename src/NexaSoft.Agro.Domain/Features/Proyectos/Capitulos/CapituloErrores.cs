using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Capitulos;

public class CapituloErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Capitulo.NoEncontrado",
        "No se encontro Capitulo"
    );

    public static readonly Error Duplicado = new
    (
        "Capitulo.Duplicado",
        "Ya existe una Capitulo con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Capitulo.ErrorSave",
        "Error al guardar Capitulo"
    );

    public static readonly Error ErrorEdit = new
    (
        "Capitulo.ErrorEdit",
        "Error al editar Capitulo"
    );

    public static readonly Error ErrorDelete = new
    (
        "Capitulo.ErrorDelete",
        "Error al eliminar Capitulo"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Capitulo.ErrorConsulta",
        "Error al listar Capitulo"
    );
}
