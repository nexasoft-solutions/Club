using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Queries.GetEstudiosReport;

public sealed record GetEstudioReportQuery(long Id): IQuery<byte[]>;

