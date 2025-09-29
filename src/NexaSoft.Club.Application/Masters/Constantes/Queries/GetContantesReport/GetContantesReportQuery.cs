using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Constantes.Queries.GetContantesReport;

public sealed record GetContantesReportQuery(
    List<string> TiposConstante
):IQuery<byte[]>;

