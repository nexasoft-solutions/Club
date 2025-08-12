using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Features.Proyectos.Capitulos;

namespace NexaSoft.Agro.Application.Features.Proyectos.Capitulos.Queries.GetCapitulo;

public sealed record GetCapituloQuery(
    Guid Id
) : IQuery<CapituloResponse>;
