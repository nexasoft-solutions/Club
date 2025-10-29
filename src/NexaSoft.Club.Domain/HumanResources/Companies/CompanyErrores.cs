using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.Companies;

public class CompanyErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Company.NoEncontrado",
        "No se encontro Company"
    );

    public static readonly Error Duplicado = new
    (
        "Company.Duplicado",
        "Ya existe una Company con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Company.ErrorSave",
        "Error al guardar Company"
    );

    public static readonly Error ErrorEdit = new
    (
        "Company.ErrorEdit",
        "Error al editar Company"
    );

    public static readonly Error ErrorDelete = new
    (
        "Company.ErrorDelete",
        "Error al eliminar Company"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Company.ErrorConsulta",
        "Error al listar Company"
    );
}
