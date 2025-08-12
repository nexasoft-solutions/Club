using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Archivos;

public class ArchivoErrores
{
    public static readonly Error NoEncontrado = new
    (
        "Archivo.NoEncontrado",
        "No se encontro Archivo"
    );

    public static readonly Error Duplicado = new
    (
        "Archivo.Duplicado",
        "Ya existe una Archivo con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "Archivo.ErrorSave",
        "Error al guardar Archivo"
    );

    public static readonly Error ErrorEdit = new
    (
        "Archivo.ErrorEdit",
        "Error al editar Archivo"
    );

    public static readonly Error ErrorDelete = new
    (
        "Archivo.ErrorDelete",
        "Error al eliminar Archivo"
    );

    public static readonly Error ErrorConsulta = new
    (
        "Archivo.ErrorConsulta",
        "Error al listar Archivo"
    );

     public static readonly Error ErrorRutaValida = new
    (
        "Archivo.ErrorRutaValida",
        "El archivo no tiene una ruta válida"
    );
}
