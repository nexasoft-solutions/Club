using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Features.Proyectos.SubCapitulos;

namespace NexaSoft.Agro.Application.Features.Proyectos.SubCapitulos.Queries.GetSubCapitulos;

public sealed record GetSubCapitulosQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<SubCapituloResponse>>;
