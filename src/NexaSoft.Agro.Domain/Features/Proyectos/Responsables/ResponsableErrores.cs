using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Responsables;

public class ResponsableErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Responsable.NoEncontrado",
        "No se encontro Responsable"
    );

    public static readonly Error Duplicado = new
    (
        "Responsable.Duplicado",
        "Ya existe una Responsable con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Responsable.ErrorSave",
        "Error al guardar Responsable"
    );

    public static readonly Error ErrorEdit = new
    (
        "Responsable.ErrorEdit",
        "Error al editar Responsable"
    );

    public static readonly Error ErrorDelete = new
    (
        "Responsable.ErrorDelete",
        "Error al eliminar Responsable"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Responsable.ErrorConsulta",
        "Error al listar Responsable"
    );
}
