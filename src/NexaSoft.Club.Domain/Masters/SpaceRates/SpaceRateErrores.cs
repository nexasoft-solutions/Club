using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.SpaceRates;

public class SpaceRateErrores
{
    public static readonly Error NoEncontrado = new
    (
        "SpaceRate.NoEncontrado",
        "No se encontro SpaceRate"
    );

    public static readonly Error Duplicado = new
    (
        "SpaceRate.Duplicado",
        "Ya existe una SpaceRate con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "SpaceRate.ErrorSave",
        "Error al guardar SpaceRate"
    );

    public static readonly Error ErrorEdit = new
    (
        "SpaceRate.ErrorEdit",
        "Error al editar SpaceRate"
    );

    public static readonly Error ErrorDelete = new
    (
        "SpaceRate.ErrorDelete",
        "Error al eliminar SpaceRate"
    );

    public static readonly Error ErrorConsulta = new
    (
        "SpaceRate.ErrorConsulta",
        "Error al listar SpaceRate"
    );
}
