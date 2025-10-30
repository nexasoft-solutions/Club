using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.IncomeTaxScales;

public class IncomeTaxScaleErrores
{
    public static readonly Error NoEncontrado = new
    (
        "IncomeTaxScale.NoEncontrado",
        "No se encontro IncomeTaxScale"
    );

    public static readonly Error Duplicado = new
    (
        "IncomeTaxScale.Duplicado",
        "Ya existe una IncomeTaxScale con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "IncomeTaxScale.ErrorSave",
        "Error al guardar IncomeTaxScale"
    );

    public static readonly Error ErrorEdit = new
    (
        "IncomeTaxScale.ErrorEdit",
        "Error al editar IncomeTaxScale"
    );

    public static readonly Error ErrorDelete = new
    (
        "IncomeTaxScale.ErrorDelete",
        "Error al eliminar IncomeTaxScale"
    );

    public static readonly Error ErrorConsulta = new
    (
        "IncomeTaxScale.ErrorConsulta",
        "Error al listar IncomeTaxScale"
    );
}
