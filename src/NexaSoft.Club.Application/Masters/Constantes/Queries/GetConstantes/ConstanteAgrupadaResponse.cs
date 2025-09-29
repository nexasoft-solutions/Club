using NexaSoft.Club.Domain.Masters.Constantes;

namespace NexaSoft.Club.Application.Masters.Constantes.Queries.GetConstantes;

public sealed record ConstanteAgrupadaResponse(
    string TipoConstante,
    List<ConstanteResponse> Data,
    int Total
);
