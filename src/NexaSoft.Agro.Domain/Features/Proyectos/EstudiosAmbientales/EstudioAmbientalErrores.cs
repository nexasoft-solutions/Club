using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales;

public class EstudioAmbientalErrores
{
    public static readonly Error NoEncontrado = new
    (
        "EstudioAmbiental.NoEncontrado",
        "No se encontro EstudioAmbiental"
    );

    public static readonly Error Duplicado = new
    (
        "EstudioAmbiental.Duplicado",
        "Ya existe una EstudioAmbiental con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "EstudioAmbiental.ErrorSave",
        "Error al guardar EstudioAmbiental"
    );

    public static readonly Error ErrorEdit = new
    (
        "EstudioAmbiental.ErrorEdit",
        "Error al editar EstudioAmbiental"
    );

    public static readonly Error ErrorDelete = new
    (
        "EstudioAmbiental.ErrorDelete",
        "Error al eliminar EstudioAmbiental"
    );

    public static readonly Error ErrorConsulta = new
    (
        "EstudioAmbiental.ErrorConsulta",
        "Error al listar EstudioAmbiental"
    );
    public static readonly Error NoHayConincidencias = new
    (
        "EstudioAmbiental.NoHayConincidencias",
        "No hay items para exportar"
    );
}
