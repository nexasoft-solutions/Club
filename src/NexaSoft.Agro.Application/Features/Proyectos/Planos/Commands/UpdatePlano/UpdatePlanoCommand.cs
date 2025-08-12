using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Planos.Commands.UpdatePlano;

public sealed record UpdatePlanoCommand(
    Guid Id,
    int EscalaId,
    string? SistemaProyeccion,
    string? NombrePlano,
    string? CodigoPlano,
    Guid ArchivoId,
    Guid ColaboradorId,
    List<UpdatePlanoDetalleCommand> Detalles
) : ICommand<bool>;
