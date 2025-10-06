using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.MemberVisits;

public class MemberVisitErrores
{
    public static readonly Error NoEncontrado = new
    (
        "MemberVisit.NoEncontrado",
        "No se encontro MemberVisit"
    );

    public static readonly Error Duplicado = new
    (
        "MemberVisit.Duplicado",
        "Ya existe una MemberVisit con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "MemberVisit.ErrorSave",
        "Error al guardar MemberVisit"
    );

    public static readonly Error ErrorEdit = new
    (
        "MemberVisit.ErrorEdit",
        "Error al editar MemberVisit"
    );

    public static readonly Error ErrorDelete = new
    (
        "MemberVisit.ErrorDelete",
        "Error al eliminar MemberVisit"
    );

    public static readonly Error ErrorConsulta = new
    (
        "MemberVisit.ErrorConsulta",
        "Error al listar MemberVisit"
    );

    public static readonly Error NoActiveVisit = new
    (
        "MemberVisit.NoActiveVisit",
        "No se encontró una visita activa"
    );
    
}
