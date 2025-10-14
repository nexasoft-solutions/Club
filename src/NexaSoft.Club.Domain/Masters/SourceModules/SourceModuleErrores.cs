using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.SourceModules;

public class SourceModuleErrores
{
    public static readonly Error NoEncontrado = new
    (
        "SourceModule.NoEncontrado",
        "No se encontro SourceModule"
    );

    public static readonly Error Duplicado = new
    (
        "SourceModule.Duplicado",
        "Ya existe una SourceModule con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "SourceModule.ErrorSave",
        "Error al guardar SourceModule"
    );

    public static readonly Error ErrorEdit = new
    (
        "SourceModule.ErrorEdit",
        "Error al editar SourceModule"
    );

    public static readonly Error ErrorDelete = new
    (
        "SourceModule.ErrorDelete",
        "Error al eliminar SourceModule"
    );

    public static readonly Error ErrorConsulta = new
    (
        "SourceModule.ErrorConsulta",
        "Error al listar SourceModule"
    );
}
