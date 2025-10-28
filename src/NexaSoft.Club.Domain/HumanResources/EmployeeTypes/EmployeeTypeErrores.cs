using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.EmployeeTypes;

public class EmployeeTypeErrores
{
    public static readonly Error NoEncontrado = new
    (
        "EmployeeType.NoEncontrado",
        "No se encontro EmployeeType"
    );

    public static readonly Error Duplicado = new
    (
        "EmployeeType.Duplicado",
        "Ya existe una EmployeeType con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "EmployeeType.ErrorSave",
        "Error al guardar EmployeeType"
    );

    public static readonly Error ErrorEdit = new
    (
        "EmployeeType.ErrorEdit",
        "Error al editar EmployeeType"
    );

    public static readonly Error ErrorDelete = new
    (
        "EmployeeType.ErrorDelete",
        "Error al eliminar EmployeeType"
    );

    public static readonly Error ErrorConsulta = new
    (
        "EmployeeType.ErrorConsulta",
        "Error al listar EmployeeType"
    );
}
