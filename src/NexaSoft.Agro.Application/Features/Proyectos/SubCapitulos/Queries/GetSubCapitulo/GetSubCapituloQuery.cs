using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Features.Proyectos.SubCapitulos;

namespace NexaSoft.Agro.Application.Features.Proyectos.SubCapitulos.Queries.GetSubCapitulo;

public sealed record GetSubCapituloQuery(
    long Id
) : IQuery<SubCapituloResponse>;
