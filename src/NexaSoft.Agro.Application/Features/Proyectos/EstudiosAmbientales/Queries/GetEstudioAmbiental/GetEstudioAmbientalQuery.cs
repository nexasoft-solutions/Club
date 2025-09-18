using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales;

namespace NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Queries.GetEstudioAmbiental;

public sealed record GetEstudioAmbientalQuery(
    long Id
) : IQuery<EstudioAmbientalResponse>;
