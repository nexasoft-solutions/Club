using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Organizaciones;

public class OrganizacionErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Organizacion.NoEncontrado",
        "No se encontro Organizacion"
    );

    public static readonly Error Duplicado = new
    (
        "Organizacion.Duplicado",
        "Ya existe una Organizacion con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Organizacion.ErrorSave",
        "Error al guardar Organizacion"
    );

    public static readonly Error ErrorEdit = new
    (
        "Organizacion.ErrorEdit",
        "Error al editar Organizacion"
    );

    public static readonly Error ErrorDelete = new
    (
        "Organizacion.ErrorDelete",
        "Error al eliminar Organizacion"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Organizacion.ErrorConsulta",
        "Error al listar Organizacion"
    );
}
