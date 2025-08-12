using NexaSoft.Agro.Application.Abstractions.Messaging;
using static NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales.Dtos.EstudioAmbientalDto;

namespace NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Queries.GetEstudioEstructurasById;

public record class GetEstudioEstructurasByIdQuery
(
    Guid Id
):IQuery<List<EstudioAmbientalEstructuraResponse>>;
