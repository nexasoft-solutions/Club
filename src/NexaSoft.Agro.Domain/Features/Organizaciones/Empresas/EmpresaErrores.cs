using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Organizaciones.Empresas;

public class EmpresaErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Empresa.NoEncontrado",
        "No se encontro Empresa"
    );

    public static readonly Error Duplicado = new
    (
        "Empresa.Duplicado",
        "Ya existe una Empresa con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Empresa.ErrorSave",
        "Error al guardar Empresa"
    );

    public static readonly Error ErrorEdit = new
    (
        "Empresa.ErrorEdit",
        "Error al editar Empresa"
    );

    public static readonly Error ErrorDelete = new
    (
        "Empresa.ErrorDelete",
        "Error al eliminar Empresa"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Empresa.ErrorConsulta",
        "Error al listar Empresa"
    );

    public static readonly Error NoHayConincidencias = new
    (
        "Empresa.NoHayConincidencias",
        "No hay items para exportar"
    );
}
