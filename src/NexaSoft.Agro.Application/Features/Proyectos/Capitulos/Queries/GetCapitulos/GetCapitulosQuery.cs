using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Features.Proyectos.Capitulos;

namespace NexaSoft.Agro.Application.Features.Proyectos.Capitulos.Queries.GetCapitulos;

public sealed record GetCapitulosQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<CapituloResponse>>;
