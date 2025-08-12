using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Masters.Consultoras.Colaboradores;

public class ColaboradorErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Colaborador.NoEncontrado",
        "No se encontro Colaborador"
    );

    public static readonly Error Duplicado = new
    (
        "Colaborador.Duplicado",
        "Ya existe una Colaborador con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Colaborador.ErrorSave",
        "Error al guardar Colaborador"
    );

    public static readonly Error ErrorEdit = new
    (
        "Colaborador.ErrorEdit",
        "Error al editar Colaborador"
    );

    public static readonly Error ErrorDelete = new
    (
        "Colaborador.ErrorDelete",
        "Error al eliminar Colaborador"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Colaborador.ErrorConsulta",
        "Error al listar Colaborador"
    );
}
