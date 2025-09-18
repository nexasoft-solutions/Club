using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Cumplimientos;

public class CumplimientoErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Cumplimiento.NoEncontrado",
        "No se encontro Cumplimiento"
    );

    public static readonly Error Duplicado = new
    (
        "Cumplimiento.Duplicado",
        "Ya existe una Cumplimiento con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Cumplimiento.ErrorSave",
        "Error al guardar Cumplimiento"
    );

    public static readonly Error ErrorEdit = new
    (
        "Cumplimiento.ErrorEdit",
        "Error al editar Cumplimiento"
    );

    public static readonly Error ErrorDelete = new
    (
        "Cumplimiento.ErrorDelete",
        "Error al eliminar Cumplimiento"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Cumplimiento.ErrorConsulta",
        "Error al listar Cumplimiento"
    );
}
