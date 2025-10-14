using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.SpacePhotos;

public class SpacePhotoErrores
{
    public static readonly Error NoEncontrado = new
    (
        "SpacePhoto.NoEncontrado",
        "No se encontro SpacePhoto"
    );

    public static readonly Error Duplicado = new
    (
        "SpacePhoto.Duplicado",
        "Ya existe una SpacePhoto con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "SpacePhoto.ErrorSave",
        "Error al guardar SpacePhoto"
    );

    public static readonly Error ErrorEdit = new
    (
        "SpacePhoto.ErrorEdit",
        "Error al editar SpacePhoto"
    );

    public static readonly Error ErrorDelete = new
    (
        "SpacePhoto.ErrorDelete",
        "Error al eliminar SpacePhoto"
    );

    public static readonly Error ErrorConsulta = new
    (
        "SpacePhoto.ErrorConsulta",
        "Error al listar SpacePhoto"
    );

    public static readonly Error PhotoFileRequired = new(
        "SpacePhoto.PhotoFileRequired",
        "El archivo de foto es requerido"
    );
}
