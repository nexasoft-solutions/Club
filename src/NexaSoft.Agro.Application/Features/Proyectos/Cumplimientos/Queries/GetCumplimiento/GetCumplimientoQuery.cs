using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Features.Proyectos.Cumplimientos;

namespace NexaSoft.Agro.Application.Features.Proyectos.Cumplimientos.Queries.GetCumplimiento;

public sealed record GetCumplimientoQuery(
    long Id
) : IQuery<CumplimientoResponse>;
