using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Planos;

public class PlanoErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Plano.NoEncontrado",
        "No se encontro Plano"
    );

    public static readonly Error Duplicado = new
    (
        "Plano.Duplicado",
        "Ya existe una Plano con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Plano.ErrorSave",
        "Error al guardar Plano"
    );

    public static readonly Error ErrorEdit = new
    (
        "Plano.ErrorEdit",
        "Error al editar Plano"
    );

    public static readonly Error ErrorDelete = new
    (
        "Plano.ErrorDelete",
        "Error al eliminar Plano"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Plano.ErrorConsulta",
        "Error al listar Plano"
    );
}
