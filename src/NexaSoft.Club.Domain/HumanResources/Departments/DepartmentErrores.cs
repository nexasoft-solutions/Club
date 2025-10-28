using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.Departments;

public class DepartmentErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Department.NoEncontrado",
        "No se encontro Department"
    );

    public static readonly Error Duplicado = new
    (
        "Department.Duplicado",
        "Ya existe una Department con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Department.ErrorSave",
        "Error al guardar Department"
    );

    public static readonly Error ErrorEdit = new
    (
        "Department.ErrorEdit",
        "Error al editar Department"
    );

    public static readonly Error ErrorDelete = new
    (
        "Department.ErrorDelete",
        "Error al eliminar Department"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Department.ErrorConsulta",
        "Error al listar Department"
    );
}
