using System;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.FeeConfigurations;

public class MemberTypeFeeErrores
{
    public static readonly Error NoEncontrado = new
    (
        "MemberTypeFeeErrores.NoEncontrado",
        "No se encontro MemberTypeFee"
    );
    public static readonly Error Duplicado = new
    (
        "MemberTypeFeeErrores.Duplicado",
        "Ya existe una MemberTypeFee con el mismo valor"
    );

    public static readonly Error ErrorSave = new
    (
        "MemberTypeFeeErrores.ErrorSave",
        "Error al guardar MemberTypeFee"
    );

    public static readonly Error ErrorConsulta = new
    (
        "MemberTypeFeeErrores.ErrorConsulta",
        "Error al listar MemberTypeFee"
    );
}
