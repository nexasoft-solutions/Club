using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Dtos;
using static NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales.Dtos.EstudioAmbientalDto;

namespace NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Queries.GetEstudioAmbientalById;

public sealed record GetEstudioAmbientalByIdQuery(
    Guid Id
): IQuery<EstudioAmbientalDtoResponse?>;
