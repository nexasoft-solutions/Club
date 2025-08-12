using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.SubCapitulos;

public class SubCapituloErrores
{
    public static readonly Error NoEncontrado = new
    (
        "SubCapitulo.NoEncontrado",
        "No se encontro SubCapitulo"
    );

    public static readonly Error Duplicado = new
    (
        "SubCapitulo.Duplicado",
        "Ya existe una SubCapitulo con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "SubCapitulo.ErrorSave",
        "Error al guardar SubCapitulo"
    );

    public static readonly Error ErrorEdit = new
    (
        "SubCapitulo.ErrorEdit",
        "Error al editar SubCapitulo"
    );

    public static readonly Error ErrorDelete = new
    (
        "SubCapitulo.ErrorDelete",
        "Error al eliminar SubCapitulo"
    );

    public static readonly Error ErrorConsulta = new
    (
        "SubCapitulo.ErrorConsulta",
        "Error al listar SubCapitulo"
    );
}
