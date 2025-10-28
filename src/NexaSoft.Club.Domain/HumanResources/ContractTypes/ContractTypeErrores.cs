using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.ContractTypes;

public class ContractTypeErrores
{
    public static readonly Error NoEncontrado = new
    (
        "ContractType.NoEncontrado",
        "No se encontro ContractType"
    );

    public static readonly Error Duplicado = new
    (
        "ContractType.Duplicado",
        "Ya existe una ContractType con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "ContractType.ErrorSave",
        "Error al guardar ContractType"
    );

    public static readonly Error ErrorEdit = new
    (
        "ContractType.ErrorEdit",
        "Error al editar ContractType"
    );

    public static readonly Error ErrorDelete = new
    (
        "ContractType.ErrorDelete",
        "Error al eliminar ContractType"
    );

    public static readonly Error ErrorConsulta = new
    (
        "ContractType.ErrorConsulta",
        "Error al listar ContractType"
    );
}
