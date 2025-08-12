using NexaSoft.Agro.Domain.Masters.Constantes;

namespace NexaSoft.Agro.Application.Masters.Constantes.Queries.GetConstantes;

public sealed record ConstanteAgrupadaResponse(
    string TipoConstante,
    List<ConstanteResponse> Data,
    int Total
);
