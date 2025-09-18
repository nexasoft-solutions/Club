using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Constantes.Queries.GetContantesReport;

public sealed record GetContantesReportQuery(
    List<string> TiposConstante
):IQuery<byte[]>;

