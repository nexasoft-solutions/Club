using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Planos.Commands.CreatePlano;

public sealed record CreatePlanoCommand(
    int EscalaId,
    string? SistemaProyeccion,
    string? NombrePlano,
    string? CodigoPlano,
    Guid ArchivoId,
    Guid ColaboradorId,
    List<CreatePlanoDetalleCommand> Detalles
) : ICommand<Guid>;
