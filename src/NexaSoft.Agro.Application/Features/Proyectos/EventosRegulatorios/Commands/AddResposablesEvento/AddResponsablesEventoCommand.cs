using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Commands.AddResposablesEvento;

public sealed record AddResponsablesEventoCommand(
    long EventoRegulatorioId,
    IEnumerable<long> ResponsablesIds,
    string UsuarioCreacion
) : ICommand<bool>;