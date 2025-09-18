using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Planos.Commands.UpdatePlano;

public sealed record UpdatePlanoCommand(
    long Id,
    int EscalaId,
    string? SistemaProyeccion,
    string? NombrePlano,
    string? CodigoPlano,
    long ArchivoId,
    long ColaboradorId,
    List<UpdatePlanoDetalleCommand> Detalles,
    string? UsuarioModificacion
) : ICommand<bool>;
