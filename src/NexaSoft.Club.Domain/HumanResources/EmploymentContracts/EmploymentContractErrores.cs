using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.EmploymentContracts;

public class EmploymentContractErrores
{
    public static readonly Error NoEncontrado = new
    (
        "EmploymentContract.NoEncontrado",
        "No se encontro EmploymentContract"
    );

    public static readonly Error Duplicado = new
    (
        "EmploymentContract.Duplicado",
        "Ya existe una EmploymentContract con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "EmploymentContract.ErrorSave",
        "Error al guardar EmploymentContract"
    );

    public static readonly Error ErrorEdit = new
    (
        "EmploymentContract.ErrorEdit",
        "Error al editar EmploymentContract"
    );

    public static readonly Error ErrorDelete = new
    (
        "EmploymentContract.ErrorDelete",
        "Error al eliminar EmploymentContract"
    );

    public static readonly Error ErrorConsulta = new
    (
        "EmploymentContract.ErrorConsulta",
        "Error al listar EmploymentContract"
    );
}
