using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales;

namespace NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Queries.GetEstudiosAmbientales;

public sealed record GetEstudiosAmbientalesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<EstudioAmbientalResponse>>;
