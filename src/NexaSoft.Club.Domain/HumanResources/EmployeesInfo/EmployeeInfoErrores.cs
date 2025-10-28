using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.EmployeesInfo;

public class EmployeeInfoErrores
{
    public static readonly Error NoEncontrado = new
    (
        "EmployeeInfo.NoEncontrado",
        "No se encontro EmployeeInfo"
    );

    public static readonly Error Duplicado = new
    (
        "EmployeeInfo.Duplicado",
        "Ya existe una EmployeeInfo con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "EmployeeInfo.ErrorSave",
        "Error al guardar EmployeeInfo"
    );

    public static readonly Error ErrorEdit = new
    (
        "EmployeeInfo.ErrorEdit",
        "Error al editar EmployeeInfo"
    );

    public static readonly Error ErrorDelete = new
    (
        "EmployeeInfo.ErrorDelete",
        "Error al eliminar EmployeeInfo"
    );

    public static readonly Error ErrorConsulta = new
    (
        "EmployeeInfo.ErrorConsulta",
        "Error al listar EmployeeInfo"
    );
}
