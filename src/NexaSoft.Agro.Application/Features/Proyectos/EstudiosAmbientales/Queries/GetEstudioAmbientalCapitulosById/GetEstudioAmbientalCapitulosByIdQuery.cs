using NexaSoft.Agro.Application.Abstractions.Messaging;
using static NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales.Dtos.EstudioAmbientalDto;

namespace NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Queries.GetEstudioAmbientalCapitulosById;

public sealed record GetEstudioAmbientalCapitulosByIdQuery
(
    Guid Id
):IQuery<EstudioAmbientalCapituloResponse>;