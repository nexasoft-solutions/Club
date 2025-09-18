using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.Planos.Commands.CreatePlano;

public sealed record CreatePlanoCommand(
    int EscalaId,
    string? SistemaProyeccion,
    string? NombrePlano,
    long ArchivoId,
    long ColaboradorId,
    List<CreatePlanoDetalleCommand> Detalles,
    string UsuarioCreacion
) : ICommand<long>;
