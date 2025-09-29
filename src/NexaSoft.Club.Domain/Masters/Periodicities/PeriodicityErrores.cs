using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.Periodicities;

public class PeriodicityErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Periodicity.NoEncontrado",
        "No se encontro Periodicity"
    );

    public static readonly Error Duplicado = new
    (
        "Periodicity.Duplicado",
        "Ya existe una Periodicity con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Periodicity.ErrorSave",
        "Error al guardar Periodicity"
    );

    public static readonly Error ErrorEdit = new
    (
        "Periodicity.ErrorEdit",
        "Error al editar Periodicity"
    );

    public static readonly Error ErrorDelete = new
    (
        "Periodicity.ErrorDelete",
        "Error al eliminar Periodicity"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Periodicity.ErrorConsulta",
        "Error al listar Periodicity"
    );
}
